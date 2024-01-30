using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Picker;
using PeanutDashboard.UnityServer.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeanutDashboard.UnityServer.SceneSync
{
	public class SceneSyncController : NetworkBehaviour
	{
		private bool _isClient;
		
		private void OnEnable()
		{
			UnityServerStartUp.ClientInstance += SetUpClientEvents;
		}

		private void SetUpClientEvents()
		{
			Debug.Log($"{nameof(SceneSyncController)}::{nameof(SetUpClientEvents)}");
			_isClient = true;
			NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		}
		
		private void OnClientConnected(ulong id)
		{
			if (_isClient){
				Debug.Log($"{nameof(SceneSyncController)}::{nameof(OnClientConnected)}");
				ClientLoadGame();
			}
		}

		private void ClientLoadGame()
		{
			Debug.Log($"{nameof(SceneSyncController)}::{nameof(ClientLoadGame)} - load: {GameNetworkSyncService.GetGamePlayScene().key}");
			ServerClientSentGame_ServerRpc(GameNetworkSyncService.GetGamePlayScene().key);
			SceneLoaderService.Instance.LoadAndOpenScene(GameNetworkSyncService.GetGamePlayScene());
		}

		[ServerRpc(RequireOwnership = false)]
		private void ServerClientSentGame_ServerRpc(string gameScene)
		{
			Debug.Log($"[SERVER-RPC]{nameof(SceneSyncController)}::{nameof(ServerClientSentGame_ServerRpc)}");
			SceneManager.LoadScene(gameScene);
		}

		private void OnDisable()
		{
			if (_isClient){
				NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
			}
			UnityServerStartUp.ClientInstance -= SetUpClientEvents;
		}
	}
}