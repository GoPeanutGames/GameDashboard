using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard.UnityServer.Events
{
	public static class ServerSyncEvents
	{
		private static UnityAction _startGameEvent;
		private static UnityAction<string> _spawnPlayerPrefab;

		public static event UnityAction StartGameEvent
		{
			add => _startGameEvent += value;
			remove => _startGameEvent -= value;
		}
		
		public static event UnityAction<string> SpawnPlayerPrefab
		{
			add => _spawnPlayerPrefab += value;
			remove => _spawnPlayerPrefab -= value;
		}

		public static void RaiseStartGameEvent()
		{
			if (_startGameEvent == null){
				LoggerService.LogWarning($"{nameof(ServerSyncEvents)}::{nameof(RaiseStartGameEvent)} raised, but nothing picked it up");
				return;
			}
			_startGameEvent.Invoke();
		}

		public static void RaiseSpawnPlayerPrefabEvent(string prefab)
		{
			if (_spawnPlayerPrefab == null){
				LoggerService.LogWarning($"{nameof(ServerSyncEvents)}::{nameof(RaiseSpawnPlayerPrefabEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnPlayerPrefab.Invoke(prefab);
		}
	}
}