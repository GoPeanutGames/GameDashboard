using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageMonsterSpawner: MonoBehaviour
    {
        private GameObject _currentMonsterPrefab;
        private int _minAmountToHave;
        private int _maxAmountToHave;
        private bool _spawnActive = false;
        private List<GameObject> _currentMonsters = new List<GameObject>();
        private float _timeToSpawn = 0.3f;
        private float _timer;

        private void OnEnable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn += OnStartWaveSpawn;
        }

        private void OnDisable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn -= OnStartWaveSpawn;
        }

        private void OnStartWaveSpawn(GameObject prefab, int min, int max)
        {
            LoggerService.LogInfo($"{nameof(RobotRampageMonsterSpawner)}::{nameof(OnStartWaveSpawn)}");
            _currentMonsterPrefab = prefab;
            _minAmountToHave = min;
            _maxAmountToHave = max;
            _spawnActive = true;
            _timer = _timeToSpawn;
        }

        private void RemoveDestroyed()
        {
            _currentMonsters.RemoveAll((m) => m == null);
        }
        
        private void Update()
        {
            RemoveDestroyed();
            if (_spawnActive)
            {
                if (_currentMonsters.Count < _minAmountToHave)
                {
                    for (int i = 0; i < _minAmountToHave - _currentMonsters.Count; i++)
                    {
                        SpawnMonster();
                    }
                }
                else if (_currentMonsters.Count < _maxAmountToHave)
                {
                    _timer -= Time.deltaTime;
                    if (_timer <= 0)
                    {
                        SpawnMonster();
                        _timer = _timeToSpawn;
                    }
                }
            }
        }

        private void SpawnMonster()
        {
            LoggerService.LogInfo($"{nameof(RobotRampageMonsterSpawner)}::{nameof(SpawnMonster)}");
            Vector3 pos = Random.insideUnitCircle.normalized * Random.Range(8f,12f);
            GameObject monster = Instantiate(_currentMonsterPrefab, pos, Quaternion.identity);
            _currentMonsters.Add(monster);
        }
    }
}