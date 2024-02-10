using System.Collections.Generic;
#if SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
#endif
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class ServerBattleDashAreaSpawner: NetworkBehaviour
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
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(OnEnable)}");
			ServerSpawnEvents.PlayerReadyBeginGame += OnPlayerReady;
			ServerAreaEvents.AreaSpawnNextArea += OnSpawnNextArea;
		}
		
		private void OnPlayerReady()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(OnPlayerReady)}");
			SpawnArea(true);
		}

		private void OnSpawnNextArea()
		{
			Debug.Log($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(OnSpawnNextArea)}");
			SpawnArea(false);
		}
		
		private void SpawnArea(bool first)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(SpawnArea)}");
			int randomIndex = 0;//Random.Range(0, _areaPrefabs.Count);
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(SpawnArea)} - index: {randomIndex}");
			GameObject prefab = _areaPrefabs[randomIndex];
			_areaPrefabs.RemoveAt(randomIndex);
			// if (!first)
			// {
			// 	_areaPrefabs.Add(_currentAreaPrefab);
			// }
			_currentAreaPrefab = prefab;
			GameObject spawnedArea = Instantiate(_currentAreaPrefab);
			spawnedArea.GetComponent<NetworkObject>().Spawn();
			spawnedArea.GetComponent<ServerAreaController>().InitialiseForStart(first);
		}

		private void OnDisable()
		{
			ServerSpawnEvents.PlayerReadyBeginGame -= OnPlayerReady;
			ServerAreaEvents.AreaSpawnNextArea -= OnSpawnNextArea;
		}
#endif
	}
}