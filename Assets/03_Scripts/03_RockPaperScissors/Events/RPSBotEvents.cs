using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSBotEvents
    {
        private static UnityAction _playStartScreenBotAnimation;
        
        public static event UnityAction OnPlayStartScreenBotAnimation
        {
            add => _playStartScreenBotAnimation += value;
            remove => _playStartScreenBotAnimation -= value;
        }

        public static void RaisePlayStartScreenBotAnimationEvent()
        {
            if (_playStartScreenBotAnimation == null){
                LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaisePlayStartScreenBotAnimationEvent)} raised, but nothing picked it up");
                return;
            }
            _playStartScreenBotAnimation.Invoke();
        }
    }
}