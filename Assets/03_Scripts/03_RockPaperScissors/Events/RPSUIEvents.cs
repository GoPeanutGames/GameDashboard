using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSUIEvents
    {
        private static UnityAction _showChooseOpponentScreen;
        public static event UnityAction OnShowChooseOpponentScreen
        {
            add => _showChooseOpponentScreen += value;
            remove => _showChooseOpponentScreen -= value;
        }

        public static void RaiseShowChooseOpponentScreenEvent()
        {
            if (_showChooseOpponentScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseShowChooseOpponentScreenEvent)} raised, but nothing picked it up");
                return;
            }
            _showChooseOpponentScreen.Invoke();
        }
    }
}