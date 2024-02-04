﻿using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Core;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Server
{
	public class ServerBattleDashPlayerSpawner : NetworkBehaviour
	{

		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _prefabToSpawn;
		
		private bool _clientReady;
		private bool _isServer;

		private void OnEnable()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerSpawner)}::{nameof(OnEnable)}");
			UnityServerStartUp.ServerInstance += SetupForServer;
		}

		private void SetupForServer()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerSpawner)}::{nameof(SetupForServer)}");
			NetworkManager.OnClientConnectedCallback += ClientConnected;
			_isServer = true;
		}

		private void ClientConnected(ulong id)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerSpawner)}::{nameof(ClientConnected)}");
			StartCoroutine(WaitUntilClientReadyToStart());
		}

		private IEnumerator WaitUntilClientReadyToStart()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerSpawner)}::{nameof(WaitUntilClientReadyToStart)}");
			while (!_clientReady){
				PingClientReady_ClientRpc();
				yield return new WaitForSeconds(0.2f);
			}
		}

		[ClientRpc]
		private void PingClientReady_ClientRpc()
		{
			LoggerService.LogInfo($"[CLIENT-RPC]{nameof(ServerBattleDashPlayerSpawner)}::{nameof(PingClientReady_ClientRpc)}");
			ClientRespondedReadyServer_ServerRpc();
		}

		[ServerRpc(RequireOwnership = false)]
		private void ClientRespondedReadyServer_ServerRpc()
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(ServerBattleDashPlayerSpawner)}::{nameof(ClientRespondedReadyServer_ServerRpc)}");
			_clientReady = true;
			SpawnPlayerVisual();
		}

		private void SpawnPlayerVisual()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerSpawner)}::{nameof(SpawnPlayerVisual)}");
			GameObject instantiatedPlayer = Instantiate(_prefabToSpawn);
			ServerSpawnEvents.RaiseSpawnedPlayerVisualEvent(instantiatedPlayer);
		}

		private void OnDisable()
		{
			if (_isServer && NetworkManager.Singleton != null){
				NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnected;
			}
		}
	}
}