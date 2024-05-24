using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageMonsterSpawner: MonoBehaviour
    {
        private const float TimeToSpawn = 0.3f;

        private List<RobotRampageWaveMonsterData> _currentWaveSetup;
        private readonly Dictionary<GameObject, List<GameObject>> _prefabCurrentMonstersList = new ();
        private bool _spawnActive = false;
        private float _timer;

        private void OnEnable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn += OnStartWaveSpawn;
        }

        private void OnDisable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn -= OnStartWaveSpawn;
        }

        private void OnStartWaveSpawn(List<RobotRampageWaveMonsterData> robotRampageMonstersData)
        {
            LoggerService.LogInfo($"{nameof(RobotRampageMonsterSpawner)}::{nameof(OnStartWaveSpawn)}");
            _currentWaveSetup = robotRampageMonstersData;
            foreach (RobotRampageWaveMonsterData robotRampageWaveMonsterData in _currentWaveSetup)
            {
                _prefabCurrentMonstersList.TryAdd(robotRampageWaveMonsterData.prefab, new List<GameObject>());
            }
            _timer = TimeToSpawn;
            _spawnActive = true;
        }

        private void RemoveDestroyed()
        {
            foreach (GameObject prefabKey in _prefabCurrentMonstersList.Keys)
            {
                _prefabCurrentMonstersList[prefabKey].RemoveAll((m) => m == null);
            }
        }
        
        private void Update()
        {
            RemoveDestroyed();
            if (_spawnActive)
            {
                foreach (RobotRampageWaveMonsterData robotRampageWaveMonsterData in _currentWaveSetup)
                {
                    int currentMonsterAmount = _prefabCurrentMonstersList[robotRampageWaveMonsterData.prefab].Count;
                    int minToSpawn = robotRampageWaveMonsterData.minToSpawn;
                    int maxToSpawn = robotRampageWaveMonsterData.maxToSpawn;
                    if (currentMonsterAmount < minToSpawn)
                    {
                        for (int i = 0; i < minToSpawn - currentMonsterAmount; i++)
                        {
                            SpawnMonster(robotRampageWaveMonsterData.prefab);
                        }
                    }
                    else if (currentMonsterAmount < maxToSpawn)
                    {
                        _timer -= Time.deltaTime;
                        if (_timer <= 0)
                        {
                            SpawnMonster(robotRampageWaveMonsterData.prefab);
                            _timer = TimeToSpawn;
                        }
                    }
                }
            }
        }

        private void SpawnMonster(GameObject prefab)
        {
            LoggerService.LogInfo($"{nameof(RobotRampageMonsterSpawner)}::{nameof(SpawnMonster)}");
            Vector3 pos = RobotRampagePlayerMovement.currentPosition + (Vector3)Random.insideUnitCircle.normalized * Random.Range(8f,12f);
            GameObject monster = Instantiate(prefab, pos, Quaternion.identity);
            _prefabCurrentMonstersList[prefab].Add(monster);
        }
    }
}