using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
    public static class RobotRampageTimerEvents
    {
        private static UnityAction<float> _startTimer;
        private static UnityAction _timerDone;
		
        public static event UnityAction<float> OnStartTimer
        {
            add => _startTimer += value;
            remove => _startTimer -= value;
        }
        
        public static event UnityAction OnTimerDone
        {
            add => _timerDone += value;
            remove => _timerDone -= value;
        }
		
        public static void RaiseStartTimerEvent(float timer)
        {
            if (_startTimer == null){
                LoggerService.LogWarning($"{nameof(RobotRampageTimerEvents)}::{nameof(RaiseStartTimerEvent)} raised, but nothing picked it up");
                return;
            }
            _startTimer.Invoke(timer);
        }
        
        public static void RaiseTimerDoneEvent()
        {
            if (_timerDone == null){
                LoggerService.LogWarning($"{nameof(RobotRampageTimerEvents)}::{nameof(RaiseTimerDoneEvent)} raised, but nothing picked it up");
                return;
            }
            _timerDone.Invoke();
        }
    }
}