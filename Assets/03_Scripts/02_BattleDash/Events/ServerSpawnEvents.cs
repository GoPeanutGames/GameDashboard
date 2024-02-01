using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ServerSpawnEvents
	{
		private static UnityAction<GameObject> _spawnedPlayerVisual;

		public static event UnityAction<GameObject> SpawnedPlayerVisual
		{
			add => _spawnedPlayerVisual += value;
			remove => _spawnedPlayerVisual -= value;
		}

		public static void RaiseSpawnedPlayerVisualEvent(GameObject visual)
		{
			if (_spawnedPlayerVisual == null){
				LoggerService.LogWarning($"{nameof(ServerSpawnEvents)}::{nameof(RaiseSpawnedPlayerVisualEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnedPlayerVisual.Invoke(visual);
		}
	}
}