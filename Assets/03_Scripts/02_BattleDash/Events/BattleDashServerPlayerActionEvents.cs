using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashServerPlayerActionEvents
	{
		private static UnityAction<Vector2> _updatePlayerAim;
		private static UnityAction<Vector2> _updatePlayerBulletSpawnPoint;

		public static event UnityAction<Vector2> OnUpdatePlayerTarget
		{
			add => _updatePlayerAim += value;
			remove => _updatePlayerAim -= value;
		}
		
		public static event UnityAction<Vector2> OnUpdatePlayerBulletSpawnPoint
		{
			add => _updatePlayerBulletSpawnPoint += value;
			remove => _updatePlayerBulletSpawnPoint -= value;
		}
		
		public static void RaiseUpdatePlayerAimEvent(Vector2 worldPosition)
		{
			if (_updatePlayerAim == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerPlayerActionEvents)}::{nameof(RaiseUpdatePlayerAimEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerAim.Invoke(worldPosition);
		}
		
		public static void RaiseUpdatePlayerBulletSpawnPointEvent(Vector2 worldPosition)
		{
			if (_updatePlayerBulletSpawnPoint == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerPlayerActionEvents)}::{nameof(RaiseUpdatePlayerBulletSpawnPointEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerBulletSpawnPoint.Invoke(worldPosition);
		}
	}
}