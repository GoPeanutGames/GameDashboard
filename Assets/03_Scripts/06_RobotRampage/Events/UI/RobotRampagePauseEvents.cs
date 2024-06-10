using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampagePauseEvents
	{
		private static UnityAction _pauseGame;
		private static UnityAction _unPauseGame;

		public static event UnityAction OnPauseGame
		{
			add => _pauseGame += value;
			remove => _pauseGame -= value;
		}
		
		public static event UnityAction OnUnPauseGame
		{
			add => _unPauseGame += value;
			remove => _unPauseGame -= value;
		}
		
		public static void RaisePauseGameEventEvent()
		{
			if (_pauseGame == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePauseEvents)}::{nameof(RaisePauseGameEventEvent)} raised, but nothing picked it up");
				return;
			}
			_pauseGame.Invoke();
		}
		
		public static void RaiseUnPauseGameEventEvent()
		{
			if (_unPauseGame == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePauseEvents)}::{nameof(RaiseUnPauseGameEventEvent)} raised, but nothing picked it up");
				return;
			}
			_unPauseGame.Invoke();
		}

	}
}