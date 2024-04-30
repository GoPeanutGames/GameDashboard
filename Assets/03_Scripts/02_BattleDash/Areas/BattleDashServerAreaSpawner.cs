using System.Collections.Generic;
using PeanutDashboard._02_BattleDash.Events;
#if SERVER
using PeanutDashboard.Shared.Logging;
#endif
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class BattleDashServerAreaSpawner: NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<GameObject> _areaPrefabs;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private GameObject _currentAreaPrefab;

#if SERVER
		private void OnEnable()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaSpawner)}::{nameof(OnEnable)}");
			BattleDashServerSpawnEvents.PlayerReadyBeginGame += OnPlayerReady;
			BattleDashServerAreaEvents.AreaSpawnNextArea += OnSpawnNextArea;
		}
		
		private void OnPlayerReady()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaSpawner)}::{nameof(OnPlayerReady)}");
			SpawnArea(true);
		}

		private void OnSpawnNextArea()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaSpawner)}::{nameof(OnSpawnNextArea)}");
			SpawnArea(false);
		}
		
		private void SpawnArea(bool first)
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaSpawner)}::{nameof(SpawnArea)}");
			int randomIndex = 0;//Random.Range(0, _areaPrefabs.Count);
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaSpawner)}::{nameof(SpawnArea)} - index: {randomIndex}");
			if (_areaPrefabs.Count == 0){ 
				SendClientWon_ClientRpc();
				BackendAuthenticationManager.SubmitGameEnd(true);
			}
			GameObject prefab = _areaPrefabs[randomIndex];
			_areaPrefabs.RemoveAt(randomIndex);
			// if (!first)
			// {
			// 	_areaPrefabs.Add(_currentAreaPrefab);
			// }
			_currentAreaPrefab = prefab;
			GameObject spawnedArea = Instantiate(_currentAreaPrefab);
			spawnedArea.GetComponent<NetworkObject>().Spawn();
			spawnedArea.GetComponent<BattleDashServerAreaController>().InitialiseForStart(first);
		}

		private void OnDisable()
		{
			BattleDashServerSpawnEvents.PlayerReadyBeginGame -= OnPlayerReady;
			BattleDashServerAreaEvents.AreaSpawnNextArea -= OnSpawnNextArea;
		}
#endif

        [ClientRpc]
		private void SendClientWon_ClientRpc()
		{
			BattleDashClientUIEvents.RaiseShowWonEvent();
		}
	}
}