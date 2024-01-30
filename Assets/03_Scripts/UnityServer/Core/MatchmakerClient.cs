using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Picker;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Matchmaker;
using Unity.Services.Matchmaker.Models;
using UnityEngine;
using AuthenticationService = Unity.Services.Authentication.AuthenticationService;

namespace PeanutDashboard.UnityServer.Core
{
	public class MatchmakerClient : MonoBehaviour
	{
		private string _ticketId;

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
			PollTicketStatus();
		}

		private async void PollTicketStatus()
		{
			MultiplayAssignment multiplayAssignment = null;
			bool gotAssignment = false;
			do{
				await Task.Delay(TimeSpan.FromSeconds(1));
				TicketStatusResponse ticketStatus = await MatchmakerService.Instance.GetTicketAsync(_ticketId);
				if (ticketStatus == null){
					continue;
				}
				if (ticketStatus.Type == typeof(MultiplayAssignment)){
					multiplayAssignment = ticketStatus.Value as MultiplayAssignment;
				}
				switch (multiplayAssignment.Status){
					case MultiplayAssignment.StatusOptions.Found:
						gotAssignment = true;
						TicketAssigned(multiplayAssignment);
						break;
					case MultiplayAssignment.StatusOptions.InProgress:
						break;
					case MultiplayAssignment.StatusOptions.Failed:
						gotAssignment = true;
						LoggerService.LogError($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - Failed to get ticket status. Error: {multiplayAssignment.Message}");
						break;
					case MultiplayAssignment.StatusOptions.Timeout:
						gotAssignment = true;
						LoggerService.LogError($"{nameof(MatchmakerClient)}::{nameof(PollTicketStatus)} - Failed to get ticket status. Ticket timed out.");
						break;
					default:
						throw new InvalidOperationException();
				}
			} while (!gotAssignment);
		}

		private void TicketAssigned(MultiplayAssignment multiplayAssignment)
		{
			LoggerService.LogInfo($"{nameof(MatchmakerClient)}::{nameof(TicketAssigned)} - Ticket assigned: {multiplayAssignment.Ip}:{multiplayAssignment.Port}");
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(multiplayAssignment.Ip, (ushort)multiplayAssignment.Port);
			NetworkManager.Singleton.StartClient();
		}
	}
}