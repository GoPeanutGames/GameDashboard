using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Picker;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Matchmaker;
using Unity.Services.Matchmaker.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using AuthenticationService = Unity.Services.Authentication.AuthenticationService;
using Player = Unity.Services.Matchmaker.Models.Player;

namespace PeanutDashboard.UnityServer.Core
{
	public class MatchmakerClient : MonoBehaviour
	{
		private string _ticketId;
		private bool _gotAssignment;

		private void OnEnable()
		{
			UnityServerStartUp.ClientInstance += SignIn;
		}

		private void SignIn()
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(SignIn)}");
			StartClient();
		}
		
		private string PlayerID()
		{
			return AuthenticationService.Instance.PlayerId;
		}

		private void OnDisable()
		{
			UnityServerStartUp.ClientInstance -= SignIn;
		}

		private void StartClient()
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(StartClient)}");
			CreateATicket();
		}

		private async void CreateATicket()
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(CreateATicket)}");
			CreateTicketOptions options = new CreateTicketOptions(GameNetworkSyncService.GetCurrentMatchmakerLabel());
			List<Player> players = new List<Player>() { new Player(PlayerID()) };
			CreateTicketResponse ticketResponse = await MatchmakerService.Instance.CreateTicketAsync(players, options);
			_ticketId = ticketResponse.Id;
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(CreateATicket)} - created ticket with id: {_ticketId}");
			StartCoroutine(PollTicketStatus());

		}

		private IEnumerator PollTicketStatus()
		{
			_gotAssignment = false;
			do{
				yield return new WaitForSeconds(1);
				Debug.Log($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - wait 1 sec");
				GetTicketStatus();
			} while (!_gotAssignment);
		}

		private async void GetTicketStatus()
		{
			Debug.Log($"{nameof(MatchmakerClient)}::{nameof(GetTicketStatus)}");
			MultiplayAssignment multiplayAssignment = null;
			TicketStatusResponse ticketStatus = await MatchmakerService.Instance.GetTicketAsync(_ticketId);
			if (ticketStatus == null){
				Debug.Log($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - ticket status = null");
				return;
			}
			if (ticketStatus.Type == typeof(MultiplayAssignment)){
				Debug.Log($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - ticket type = multiplay assignment");
				multiplayAssignment = ticketStatus.Value as MultiplayAssignment;
			}
			switch (multiplayAssignment.Status){
				case MultiplayAssignment.StatusOptions.Found:
					_gotAssignment = true;
					TicketAssigned(multiplayAssignment);
					break;
				case MultiplayAssignment.StatusOptions.InProgress:
					Debug.Log($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - In progress");
					break;
				case MultiplayAssignment.StatusOptions.Failed:
					_gotAssignment = true;
					LoggerService.LogError($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - Failed to get ticket status. Error: {multiplayAssignment.Message}");
					break;
				case MultiplayAssignment.StatusOptions.Timeout:
					_gotAssignment = true;
					LoggerService.LogError($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - Failed to get ticket status. Ticket timed out.");
					break;
				default:
					throw new InvalidOperationException();
			}
		}

		private async Task TicketAssigned(MultiplayAssignment multiplayAssignment)
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(TicketAssigned)} - Ticket assigned: {multiplayAssignment.Ip}:{multiplayAssignment.Port}");
			StartCoroutine(PingForLobby());
		}

		bool lobbyAssigned = false;
		private IEnumerator PingForLobby()
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PingForLobby)}");
			while (!lobbyAssigned){
				PollLobbies();
				yield return new WaitForSeconds(0.2f);
			}
		}

		private async Task PollLobbies()
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PollLobbies)}");
			var options = new QueryLobbiesOptions();
			options.Count = 25;
			options.Filters = new List<QueryFilter>()
			{
				new QueryFilter(
					field: QueryFilter.FieldOptions.AvailableSlots,
					op: QueryFilter.OpOptions.GT,
					value: "0"
				),
				new QueryFilter(
					field: QueryFilter.FieldOptions.IsLocked,
					op: QueryFilter.OpOptions.EQ,
					value: "0"
				)
			};
			var lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PollLobbies)} - lobbies: {lobbies.Results.Count}");
			if (lobbies.Results.Count > 0){
				LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PollLobbies)} - entering lobby");
				Lobby lobby = lobbies.Results[0];
				var joiningLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
				LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PollLobbies)} - entering lobby - {lobby.Id}");
				string joinCode = joiningLobby.Data["joinCode"].Value;
				JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
				NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "wss"));
				NetworkManager.Singleton.StartClient();
				LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(PollLobbies)} - client started");
				lobbyAssigned = true;
			}
		}
	}
}