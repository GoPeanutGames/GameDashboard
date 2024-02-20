using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Core;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Spawner
{
	public class BattleDashServerPlayerSpawner : NetworkBehaviour
	{

		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _prefabToSpawn;
		
		private bool _clientReady;
		private bool _isServer;

		private void OnEnable()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerSpawner)}::{nameof(OnEnable)}");
			UnityServerStartUp.ServerInstance += SetupForServer;
		}

		private void SetupForServer()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerSpawner)}::{nameof(SetupForServer)}");
			NetworkManager.OnClientConnectedCallback += ClientConnected;
			_isServer = true;
		}

		private void ClientConnected(ulong id)
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerSpawner)}::{nameof(ClientConnected)}");
			StartCoroutine(WaitUntilClientReadyToStart());
		}

		private IEnumerator WaitUntilClientReadyToStart()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerSpawner)}::{nameof(WaitUntilClientReadyToStart)}");
			while (!_clientReady){
				PingClientReady_ClientRpc();
				yield return new WaitForSeconds(0.2f);
			}
		}

		[ClientRpc]
		private void PingClientReady_ClientRpc()
		{
			LoggerService.LogInfo($"[CLIENT-RPC]{nameof(BattleDashServerPlayerSpawner)}::{nameof(PingClientReady_ClientRpc)}");
			ClientRespondedReadyServer_ServerRpc();
		}

		[ClientRpc]
		private void ShowPlayerTooltips_ClientRpc()
		{
			LoggerService.LogInfo($"[CLIENT-RPC]{nameof(BattleDashServerPlayerSpawner)}::{nameof(ShowPlayerTooltips_ClientRpc)}");
			BattleDashClientUIEvents.RaiseShowTooltipsEvent(true);
			BattleDashLoadingEvents.RaiseCloseLoadingEvent();
		}

		[ServerRpc(RequireOwnership = false)]
		private void ClientRespondedReadyServer_ServerRpc()
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashServerPlayerSpawner)}::{nameof(ClientRespondedReadyServer_ServerRpc)}");
			if (_clientReady){
				return;
			}
			_clientReady = true;
			BattleDashServerSpawnEvents.RaisePlayerReadyBeginGameEvent();
			SpawnPlayerVisual();
			ShowPlayerTooltips_ClientRpc();
		}

		private void SpawnPlayerVisual()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerSpawner)}::{nameof(SpawnPlayerVisual)}");
			GameObject instantiatedPlayer = Instantiate(_prefabToSpawn);
			BattleDashServerSpawnEvents.RaiseSpawnedPlayerVisualEvent(instantiatedPlayer);
		}

		private void OnDisable()
		{
			UnityServerStartUp.ServerInstance -= SetupForServer;
			if (_isServer && NetworkManager.Singleton != null){
				NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnected;
			}
		}
	}
}