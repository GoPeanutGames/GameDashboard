using System;
using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class ClientAreaEnvSpawner: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<GameObject> _envObjectPrefabs;

		[SerializeField]
		private Vector2 _minMaxX;
		
		[SerializeField]
		private Vector2 _minMaxY;
		
		[SerializeField]
		private float _minTimeToSpawn;
		
		[SerializeField]
		private float _maxTimeToSpawn;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _currentTimer;

#if !SERVER
		private void Awake()
		{
			_currentTimer = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
		}

		private void Update()
		{
			if (_envObjectPrefabs.Count == 0){
				return;
			}
			_currentTimer -= Time.deltaTime;
			if (_currentTimer <= 0){
				_currentTimer = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
				SpawnRandomEnvObject();
			}
		}

		private void SpawnRandomEnvObject()
		{
			LoggerService.LogInfo($"{nameof(ClientAreaEnvSpawner)}::{nameof(SpawnRandomEnvObject)}");
			Vector3 position = new Vector3(
				Random.Range(_minMaxX.x, _minMaxX.y),
				Random.Range(_minMaxY.x, _minMaxY.y),
				0);
			int index = Random.Range(0, _envObjectPrefabs.Count);
			Instantiate(_envObjectPrefabs[index], position, Quaternion.identity);
		}
#endif
	}
}