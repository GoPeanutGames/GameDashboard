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
		
#if SERVER
		private void OnEnable()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(OnEnable)}");
			ServerSpawnEvents.PlayerReadyBeginGame += OnPlayerReady;
		}
		
		private void OnPlayerReady()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(OnPlayerReady)}");
			SpawnArea();
		}

		private void SpawnArea()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashAreaSpawner)}::{nameof(SpawnArea)}");
			GameObject randomPrefab = _areaPrefabs[Random.Range(0, _areaPrefabs.Count)];
			GameObject spawnedArea = Instantiate(randomPrefab);
			spawnedArea.GetComponent<NetworkObject>().Spawn();
			spawnedArea.GetComponent<ServerAreaController>().InitialiseForStart();
		}

		private void OnDisable()
		{
			ServerSpawnEvents.PlayerReadyBeginGame -= OnPlayerReady;
		}
#endif
	}
}