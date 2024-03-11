using System.Collections.Generic;
using PeanutDashboard._02_BattleDash.Config;
using PeanutDashboard._02_BattleDash.Data;
#if SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Shared.Logging;
#endif
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class BattleDashServerAreaMonsterSpawner: NetworkBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private BattleDashAreaMonsterSpawnDefinition _spawnDefinition;

		private List<BattleDashMonsterSpawnPattern> _currentPatterns;
#if SERVER
		public void Initialise()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaMonsterSpawner)}::{nameof(Initialise)}");
			_currentPatterns = new List<BattleDashMonsterSpawnPattern>();
			_spawnDefinition.ResetArea();
			BattleDashServerAreaEvents.AreaDistancePassedPercUpdated += OnAreaDistancePercUpdated;
		}

		private void OnAreaDistancePercUpdated(float perc)
		{
			CheckForSpawnInCurrentPatterns();
			CheckForNewPatterns(perc);
		}

		private void SpawnMonster(BattleDashMonsterSpawnLocation spawnLocation, BattleDashMonsterType type)
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaMonsterSpawner)}::{nameof(SpawnMonster)} - type: {type.name}, at {spawnLocation.location}");
			GameObject monster = Instantiate(type.monsterPrefab, spawnLocation.location, Quaternion.identity);
			monster.GetComponent<NetworkObject>().Spawn();
		}
		
		private void CheckForSpawnInCurrentPatterns()
		{
			foreach (BattleDashMonsterSpawnPattern monsterSpawnPattern in _currentPatterns){
				if (monsterSpawnPattern.timeToBeginSpawn <= 0){
					monsterSpawnPattern.timeElapsed += NetworkManager.ServerTime.FixedDeltaTime;
					if (monsterSpawnPattern.timeElapsed > monsterSpawnPattern.timeBetweenSpawns){
						monsterSpawnPattern.timeElapsed = 0;
						monsterSpawnPattern.amount--;
						SpawnMonster(monsterSpawnPattern.battleDashMonsterSpawnLocation, monsterSpawnPattern.battleDashMonsterType);
					}
				}
				else{
					monsterSpawnPattern.timeToBeginSpawn -= NetworkManager.ServerTime.FixedDeltaTime;
				}
			}
			_currentPatterns.RemoveAll((pattern) => pattern.amount == 0);
		}

		private void CheckForNewPatterns(float perc)
		{
			foreach (BattleDashAreaMonsterSpawn spawnDefinitionAreaMonsterSpawn in _spawnDefinition.areaMonsterSpawns){
				if (spawnDefinitionAreaMonsterSpawn.areaPercentage <= perc && !spawnDefinitionAreaMonsterSpawn.spawned){
					Debug.Log($"{nameof(BattleDashServerAreaMonsterSpawner)}::{nameof(CheckForNewPatterns)} - adding pattern at perc: {spawnDefinitionAreaMonsterSpawn.areaPercentage}");
					spawnDefinitionAreaMonsterSpawn.spawned = true;
					_currentPatterns.AddRange(spawnDefinitionAreaMonsterSpawn.GetPatterns());
					return;
				}
			}
		}

		public override void OnDestroy()
		{
			BattleDashServerAreaEvents.AreaDistancePassedPercUpdated -= OnAreaDistancePercUpdated;
		}
#endif
	}
}