using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSBotEvents
    {
        private static UnityAction _playStartScreenBotAnimation;
        private static UnityAction _hideBots;
        private static UnityAction _showBots;
        
        public static event UnityAction OnPlayStartScreenBotAnimation
        {
            add => _playStartScreenBotAnimation += value;
            remove => _playStartScreenBotAnimation -= value;
        }
        
        public static event UnityAction OnHideBots
        {
            add => _hideBots += value;
            remove => _hideBots -= value;
        }

        public static event UnityAction OnShowBots
        {
            add => _showBots += value;
            remove => _showBots -= value;
        }

        public static void RaisePlayStartScreenBotAnimationEvent()
        {
            if (_playStartScreenBotAnimation == null){
                LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaisePlayStartScreenBotAnimationEvent)} raised, but nothing picked it up");
                return;
            }
            _playStartScreenBotAnimation.Invoke();
        }

        public static void RaiseHideBotsEvent()
        {
            if (_hideBots == null){
                LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseHideBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _hideBots.Invoke();
        }
        
        public static void RaiseShowBotsEvent()
        {
            if (_showBots == null){
                LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseShowBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _showBots.Invoke();
        }
    }
}