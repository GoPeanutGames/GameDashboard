using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageMonsterSpawner: MonoBehaviour
    {
        private const float TimeToSpawn = 0.3f;

        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private float _minSpawnCircleRadius;
        
        [SerializeField]
        private float _maxSpawnCircleRadius;
        
        [SerializeField]
        private MobCollection _mobCollection;
        
        private List<RobotRampageSubWaveTrigger> _currentSubWavesSetup;
        private List<RobotRampageSubWaveTrigger> _activeSubWaves;
        private readonly Dictionary<MobType, GameObject> _mobTypePrefabMap = new ();
        private readonly Dictionary<MobType, List<GameObject>> _mobTypeCurrentMonstersMap = new ();
        private bool _spawnActive = false;
        private float _timerToSpawn;
        private float _currentWaveTimer;

        private void OnEnable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn += OnStartWaveSpawn;
        }

        private void OnDisable()
        {
            RobotRampageWaveEvents.OnStartWaveSpawn -= OnStartWaveSpawn;
        }

        private void OnStartWaveSpawn(List<RobotRampageSubWaveTrigger> robotRampageMonstersData)
        {
            LoggerService.LogInfo($"{nameof(RobotRampageMonsterSpawner)}::{nameof(OnStartWaveSpawn)}");
            _currentWaveTimer = 0;
            _currentSubWavesSetup = robotRampageMonstersData;
            _activeSubWaves = new List<RobotRampageSubWaveTrigger>();
            foreach (RobotRampageSubWaveTrigger robotRampageWaveMonsterData in _currentSubWavesSetup)
            {
                foreach (RobotRampageMonsterSpawnConfig robotRampageMonsterSpawnConfig in robotRampageWaveMonsterData.spawnConfig){
                    GameObject prefab = _mobCollection.GetPrefabForMonster(robotRampageMonsterSpawnConfig.mobType);
                    _mobTypePrefabMap.TryAdd(robotRampageMonsterSpawnConfig.mobType, prefab);
                    _mobTypeCurrentMonstersMap.TryAdd(robotRampageMonsterSpawnConfig.mobType, new List<GameObject>());
                }
            }
            _timerToSpawn = TimeToSpawn;
            _spawnActive = true;
        }

        private void RemoveDestroyed()
        {
            foreach (MobType mobTypeKey in _mobTypeCurrentMonstersMap.Keys)
            {
                _mobTypeCurrentMonstersMap[mobTypeKey].RemoveAll((m) => m == null);
            }
        }

        private void UpdateActiveSubWaves()
        {
            for (int i = 0; i < _currentSubWavesSetup.Count; i++){
                if (_currentWaveTimer > _currentSubWavesSetup[i].timeToStart){
                    if (!_currentSubWavesSetup[i].keepLast){
                        _activeSubWaves.Clear();
                    }
                    _activeSubWaves.Add(_currentSubWavesSetup[i]);
                }
            }
        }
        
        private void Update()
        {
            RemoveDestroyed();
            if (_spawnActive){
                _currentWaveTimer += Time.deltaTime;
                UpdateActiveSubWaves();
                foreach (RobotRampageSubWaveTrigger robotRampageSubWaveTrigger in _activeSubWaves){
                    foreach (RobotRampageMonsterSpawnConfig robotRampageMonsterSpawnConfig in robotRampageSubWaveTrigger.spawnConfig){
                        int currentMonsterAmount = _mobTypeCurrentMonstersMap[robotRampageMonsterSpawnConfig.mobType].Count;
                        int minToSpawn = robotRampageMonsterSpawnConfig.minToSpawn;
                        int maxToSpawn = robotRampageMonsterSpawnConfig.maxToSpawn;
                        if (currentMonsterAmount < minToSpawn){
                            for (int i = 0; i < minToSpawn - currentMonsterAmount; i++){
                                SpawnMonster(robotRampageMonsterSpawnConfig.mobType, _mobTypePrefabMap[robotRampageMonsterSpawnConfig.mobType]);
                            }
                        }
                        else if (currentMonsterAmount < maxToSpawn){
                            _timerToSpawn -= Time.deltaTime;
                            if (_timerToSpawn <= 0){
                                SpawnMonster(robotRampageMonsterSpawnConfig.mobType, _mobTypePrefabMap[robotRampageMonsterSpawnConfig.mobType]);
                                _timerToSpawn = TimeToSpawn;
                            }
                        }
                    }
                }
            }
        }

        private void SpawnMonster(MobType mobType, GameObject prefab)
        {
            Vector3 pos = RobotRampagePlayerController.currentPosition + (Vector3)Random.insideUnitCircle.normalized * Random.Range(_minSpawnCircleRadius, _maxSpawnCircleRadius);
            GameObject monster = Instantiate(prefab, pos, Quaternion.identity); 
            _mobTypeCurrentMonstersMap[mobType].Add(monster);
        }
    }
}