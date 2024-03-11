using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashServerGameStateEvents
	{
		private static UnityAction _pauseTriggered;
		private static UnityAction _unPauseTriggered;

		public static event UnityAction OnPauseTriggered
		{
			add => _pauseTriggered += value;
			remove => _pauseTriggered -= value;
		}
		
		public static event UnityAction OnUnPauseTriggered
		{
			add => _unPauseTriggered += value;
			remove => _unPauseTriggered -= value;
		}
		
		public static void RaisePauseTriggeredEvent()
		{
			if (_pauseTriggered == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerGameStateEvents)}::{nameof(RaisePauseTriggeredEvent)} raised, but nothing picked it up");
				return;
			}
			_pauseTriggered.Invoke();
		}
		
		public static void RaiseUnPauseTriggeredEvent()
		{
			if (_unPauseTriggered == null){
				LoggerService.LogWarning($"{nameof(BattleDashServerGameStateEvents)}::{nameof(RaiseUnPauseTriggeredEvent)} raised, but nothing picked it up");
				return;
			}
			_unPauseTriggered.Invoke();
		}
	}
}