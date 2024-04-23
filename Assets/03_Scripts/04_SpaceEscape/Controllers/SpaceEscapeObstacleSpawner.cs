using System;
using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceEscapeObstacleSpawner : MonoBehaviour
{
    [Header(InspectorNames.SetInInspector)]
    [SerializeField]
    private List<Transform> _lowSpawnPoints;
    [SerializeField]
    private List<Transform> _midSpawnPoints;
    [SerializeField]
    private List<Transform> _highSpawnPoints;
    [SerializeField]
    private GameObject _obstaclePrefab;
    [SerializeField]
    private float _timeToSpawnMin = 1f;
    [SerializeField]
    private float _timeToSpawnMax = 3f;

    [Header(InspectorNames.DebugDynamic)]
    private float _timeToSpawn = 0;
    
    private void Update()
    {
        _timeToSpawn -= Time.deltaTime;
        if (_timeToSpawn <= 0){
            _timeToSpawn = Random.Range(_timeToSpawnMin, _timeToSpawnMax);
            float _height = Random.Range(0f, 1f);
            if (_height < 0.33f){
                int indexSide = Random.Range(0, 3);
                GameObject.Instantiate(_obstaclePrefab, _lowSpawnPoints[indexSide].position, Quaternion.identity);
            }
            else if (_height < 0.66f){
                int indexSide = Random.Range(0, 3);
                GameObject.Instantiate(_obstaclePrefab, _midSpawnPoints[indexSide].position, Quaternion.identity);
            }
            else{
                int indexSide = Random.Range(0, 3);
                GameObject.Instantiate(_obstaclePrefab, _highSpawnPoints[indexSide].position, Quaternion.identity);
            }
        }
    }
}
