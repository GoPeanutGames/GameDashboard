using System.Collections.Generic;
using PeanutDashboard._02_BattleDash.Config;
using PeanutDashboard._02_BattleDash.Data;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class ServerAreaMonsterSpawner: NetworkBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private AreaMonsterSpawnDefinition _spawnDefinition;

		private List<MonsterSpawnPattern> _currentPatterns;
		
		private Vector3 _endPos;
		private float _startingDistance;
		private GameObject _background;
		
#if SERVER
		public void Initialise(GameObject background)
		{
			Debug.Log($"{nameof(ServerAreaMonsterSpawner)}::{nameof(Initialise)}");
			_background = background;
			_currentPatterns = new List<MonsterSpawnPattern>();
			_spawnDefinition.ResetArea();
			Vector3 localPosition = _background.transform.localPosition;
			_endPos = -localPosition;
			_startingDistance = Vector3.Distance(localPosition, _endPos);
		}

		private void Update()
		{
			CheckForSpawnInCurrentPatterns();
			CheckForNewPatterns();
		}

		private void SpawnMonster(MonsterSpawnLocation spawnLocation, MonsterType type)
		{
			Debug.Log($"{nameof(ServerAreaMonsterSpawner)}::{nameof(SpawnMonster)} - type: {type.name}, at {spawnLocation.location}");
			GameObject monster = Instantiate(type.monsterPrefab, spawnLocation.location, Quaternion.identity);
			monster.GetComponent<NetworkObject>().Spawn();
		}
		
		private void CheckForSpawnInCurrentPatterns()
		{
			foreach (MonsterSpawnPattern monsterSpawnPattern in _currentPatterns){
				if (monsterSpawnPattern.timeToBeginSpawn <= 0){
					monsterSpawnPattern.timeElapsed += NetworkManager.ServerTime.FixedDeltaTime;
					if (monsterSpawnPattern.timeElapsed > monsterSpawnPattern.timeBetweenSpawns){
						monsterSpawnPattern.timeElapsed = 0;
						monsterSpawnPattern.amount--;
						SpawnMonster(monsterSpawnPattern.monsterSpawnLocation, monsterSpawnPattern.monsterType);
					}
				}
				else{
					monsterSpawnPattern.timeToBeginSpawn -= NetworkManager.ServerTime.FixedDeltaTime;
				}
			}
			_currentPatterns.RemoveAll((pattern) => pattern.amount == 0);
		}

		private void CheckForNewPatterns()
		{
			float currentDistance = Vector3.Distance(_background.transform.localPosition, _endPos);
			float percentage = 1 - currentDistance / _startingDistance;
			foreach (AreaMonsterSpawn spawnDefinitionAreaMonsterSpawn in _spawnDefinition.areaMonsterSpawns){
				if (spawnDefinitionAreaMonsterSpawn.areaPercentage <= percentage && !spawnDefinitionAreaMonsterSpawn.spawned){
					Debug.Log($"{nameof(ServerAreaMonsterSpawner)}::{nameof(CheckForNewPatterns)} - adding pattern at perc: {spawnDefinitionAreaMonsterSpawn.areaPercentage}");
					spawnDefinitionAreaMonsterSpawn.spawned = true;
					_currentPatterns.AddRange(spawnDefinitionAreaMonsterSpawn.GetPatterns());
					return;
				}
			}
		}
#endif
	}
}