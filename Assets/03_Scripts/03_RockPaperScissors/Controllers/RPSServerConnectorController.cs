using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard._03_RockPaperScissors.UnityServer;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSServerConnectorController: NetworkBehaviour
	{
#if !SERVER
		private void Start()
		{
			Debug.Log($"{nameof(RPSServerConnectorController)}::{nameof(Start)}");
			if (IsLocalPlayer){
				SendStartingValueToServer_ServerRpc(RPSCurrentClientState.rpsChoiceType, this.OwnerClientId);
			}
		}
#endif

#if SERVER
		private void OnEnable()
		{
			RPSServerEvents.SendOtherChoiceToPlayer += ServerReturnedResult;
		}

		private void OnDisable()
		{
			RPSServerEvents.SendOtherChoiceToPlayer -= ServerReturnedResult;
		}
		
#else
		private void OnEnable()
		{
			RPSServerEvents.SendChoiceToServer += OnSendChoiceToServer;
		}

		private void OnDisable()
		{
			RPSServerEvents.SendChoiceToServer -= OnSendChoiceToServer;
		}
#endif

		private void OnSendChoiceToServer()
		{
			if (!IsLocalPlayer){
				return;
			}
			Debug.Log($"{nameof(RPSServerConnectorController)}::{nameof(OnSendChoiceToServer)}");
			SendStartingValueToServer_ServerRpc(RPSCurrentClientState.rpsChoiceType, this.OwnerClientId);
		}

		private void ServerReturnedResult(ulong clientId, bool wonGame, bool endgame, RPSChoiceType otherChoice)
		{
			ClientRpcParams clientRpcParams = new ClientRpcParams
			{
				Send = new ClientRpcSendParams
				{
					TargetClientIds = new ulong[]{clientId}
				}
			};

			SendToClientResult_ClientRpc(otherChoice, wonGame, endgame, clientRpcParams);
		}
		

		[ServerRpc (RequireOwnership = false)]
		private void SendStartingValueToServer_ServerRpc(RPSChoiceType choiceType, ulong clientId)
		{
			Debug.Log($"[SERVER]{nameof(RPSServerConnectorController)}::{nameof(SendStartingValueToServer_ServerRpc)} - {choiceType}");
			RPSServerLogic.PlayerMadeChoice(choiceType, clientId);
		}

		[ClientRpc]
		private void SendToClientResult_ClientRpc(RPSChoiceType otherChoice, bool wonGame, bool endgame, ClientRpcParams clientRpcParams = default)
		{
			Debug.Log($"[CLIENT]{nameof(RPSServerConnectorController)}::{nameof(SendToClientResult_ClientRpc)} - other: {otherChoice}");
			RPSServerEvents.RaiseOpponentChoiceEvent(otherChoice, wonGame, endgame);
		}
	}
}