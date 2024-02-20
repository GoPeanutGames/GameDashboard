using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashServerSpawnEvents
	{
		private static UnityAction<GameObject> _spawnedPlayerVisual;
		private static UnityAction _playerReadyBeginGame;

		public static event UnityAction<GameObject> SpawnedPlayerVisual
		{
			add => _spawnedPlayerVisual += value;
			remove => _spawnedPlayerVisual -= value;
		}
		
		public static event UnityAction PlayerReadyBeginGame
		{
			add => _playerReadyBeginGame += value;
			remove => _playerReadyBeginGame -= value;
		}

		public static void RaiseSpawnedPlayerVisualEvent(GameObject visual)
		{
			if (_spawnedPlayerVisual == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerSpawnEvents)}::{nameof(RaiseSpawnedPlayerVisualEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnedPlayerVisual.Invoke(visual);
		}

		public static void RaisePlayerReadyBeginGameEvent()
		{
			if (_playerReadyBeginGame == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerSpawnEvents)}::{nameof(RaisePlayerReadyBeginGameEvent)} raised, but nothing picked it up");
				return;
			}
			_playerReadyBeginGame.Invoke();
		}
	}
}