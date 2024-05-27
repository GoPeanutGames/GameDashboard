using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampagePlayerEvents
	{
		private static UnityAction _playerKilled;
		
		public static event UnityAction OnPlayerKilled
		{
			add => _playerKilled += value;
			remove => _playerKilled -= value;
		}
		
		public static void RaisePlayerKilledEvent()
		{
			if (_playerKilled == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePlayerEvents)}::{nameof(RaisePlayerKilledEvent)} raised, but nothing picked it up");
				return;
			}
			_playerKilled.Invoke();
		}
	}
}