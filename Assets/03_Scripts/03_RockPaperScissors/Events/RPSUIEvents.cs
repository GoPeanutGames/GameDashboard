using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSUIEvents
    {
        private static UnityAction _showChooseOpponentScreen;
        private static UnityAction _showChooseModeScreen;
        
        public static event UnityAction OnShowChooseOpponentScreen
        {
            add => _showChooseOpponentScreen += value;
            remove => _showChooseOpponentScreen -= value;
        }
        
        public static event UnityAction OnShowChooseModeScreen
        {
            add => _showChooseModeScreen += value;
            remove => _showChooseModeScreen -= value;
        }

        public static void RaiseShowChooseOpponentScreenEvent()
        {
            if (_showChooseOpponentScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseShowChooseOpponentScreenEvent)} raised, but nothing picked it up");
                return;
            }
            _showChooseOpponentScreen.Invoke();
        }

        public static void RaiseShowChooseModeScreenEvent()
        {
            if (_showChooseModeScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseShowChooseModeScreenEvent)} raised, but nothing picked it up");
                return;
            }
            _showChooseModeScreen.Invoke();
        }
    }
}