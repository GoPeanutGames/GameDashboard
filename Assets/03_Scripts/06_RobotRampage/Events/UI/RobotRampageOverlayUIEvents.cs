using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageOverlayUIEvents
	{
		private static UnityAction<Vector3, float> _spawnDamageIndicator;

		public static event UnityAction<Vector3, float> OnSpawnDamageIndicator
		{
			add => _spawnDamageIndicator += value;
			remove => _spawnDamageIndicator -= value;
		}
		
		public static void RaiseSpawnDamageIndicatorEvent(Vector3 worldPosition, float damage)
		{
			if (_spawnDamageIndicator == null){
				LoggerService.LogWarning($"{nameof(RobotRampageOverlayUIEvents)}::{nameof(RaiseSpawnDamageIndicatorEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnDamageIndicator.Invoke(worldPosition, damage);
		}
	}
}