using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageExpSpawnEvents
	{
		private static UnityAction<RobotRampageExpType, Vector3> _spawnExpType;
		
		public static event UnityAction<RobotRampageExpType, Vector3> OnSpawnExpType
		{
			add => _spawnExpType += value;
			remove => _spawnExpType -= value;
		}
		
		public static void RaiseSpawnExpTypeEvent(RobotRampageExpType expType, Vector3 position)
		{
			if (_spawnExpType == null){
				LoggerService.LogWarning($"{nameof(RobotRampageExpSpawnEvents)}::{nameof(RaiseSpawnExpTypeEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnExpType.Invoke(expType, position);
		}
	}
}