#if SERVER
using System.Collections;
#endif
using PeanutDashboard.UnityServer.Events;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard.UnityServer.SceneSync
{
	public class GameStartSync: NetworkBehaviour
	{
		private bool _clientReady;

#if SERVER
		private void Start()
		{
			Debug.Log($"{nameof(GameStartSync)}::{nameof(Start)}");
			if (NetworkManager.Singleton.IsServer){
				StartCoroutine(PingClientForStart());
			}
		}

		
		IEnumerator PingClientForStart()
		{
			Debug.Log($"{nameof(GameStartSync)}::{nameof(PingClientForStart)}");
			while (!_clientReady){
				ClientRespondReady_ClientRpc();
				yield return new WaitForSeconds(0.5f);
			}
		}

#endif
		[ClientRpc]
		private void ClientRespondReady_ClientRpc()
		{
			Debug.Log($"[CLIENT-RPC]{nameof(GameStartSync)}::{nameof(ClientRespondReady_ClientRpc)}");
			PingServerClientReady_ServerRpc();
		}

		[ServerRpc(RequireOwnership = false)]
		private void PingServerClientReady_ServerRpc()
		{
			Debug.Log($"[SERVER-RPC]{nameof(GameStartSync)}::{nameof(PingServerClientReady_ServerRpc)}");
			_clientReady = true;
			ServerSyncEvents.RaiseStartGameEvent();
		}
	}
}