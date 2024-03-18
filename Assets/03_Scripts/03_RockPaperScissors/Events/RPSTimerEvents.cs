using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSTimerEvents
	{
		private static UnityAction<float> _startTimer;
        
		public static event UnityAction<float> OnStartTimer
		{
			add => _startTimer += value;
			remove => _startTimer -= value;
		}

		public static void RaiseStartTimerEvent(float time)
		{
			if (_startTimer == null){
				LoggerService.LogWarning($"{nameof(RPSTimerEvents)}::{nameof(RaiseStartTimerEvent)} raised, but nothing picked it up");
				return;
			}
			_startTimer.Invoke(time);
		}
	}
}