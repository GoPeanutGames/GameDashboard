using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class EffectEvents
	{
		private static UnityAction<EffectType, Vector3> _spawnEffectAt;
		
		public static event UnityAction<EffectType, Vector3> OnSpawnEffectAt
		{
			add => _spawnEffectAt += value;
			remove => _spawnEffectAt -= value;
		}
		
		public static void RaiseSpawnEffectAt(EffectType effectType, Vector3 position)
		{
			if (_spawnEffectAt == null){
				LoggerService.LogWarning($"{nameof(EffectEvents)}::{nameof(RaiseSpawnEffectAt)} raised, but nothing picked it up");
				return;
			}
			_spawnEffectAt.Invoke(effectType, position);
		}
	}
}