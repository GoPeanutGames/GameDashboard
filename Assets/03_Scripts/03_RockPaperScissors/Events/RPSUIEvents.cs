using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSUIEvents
    {
        private static UnityAction _showStartScreen;
        private static UnityAction _showChooseOpponentScreen;
        private static UnityAction _showChooseModeScreen;
        private static UnityAction _showGameChooseOptionScreen;
        private static UnityAction _hideWonScreen;
        private static UnityAction _playButtonClick;
        
        public static event UnityAction OnShowStartScreen
        {
            add => _showStartScreen += value;
            remove => _showStartScreen -= value;
        }
        
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
        
        public static event UnityAction OnShowGameChooseOptionScreen
        {
            add => _showGameChooseOptionScreen += value;
            remove => _showGameChooseOptionScreen -= value;
        }
        
        public static event UnityAction OnHideWonScreen
        {
            add => _hideWonScreen += value;
            remove => _hideWonScreen -= value;
        }
        
        public static event UnityAction OnPlayButtonClick
        {
            add => _playButtonClick += value;
            remove => _playButtonClick -= value;
        }

        public static void RaiseShowStartScreenEvent()
        {
            if (_showStartScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseShowStartScreenEvent)} raised, but nothing picked it up");
                return;
            }
            _showStartScreen.Invoke();
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

        public static void RaiseGameShowChooseOptionScreenEvent()
        {
            if (_showGameChooseOptionScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseGameShowChooseOptionScreenEvent)} raised, but nothing picked it up");
                return;
            }
            _showGameChooseOptionScreen.Invoke();
        }

        public static void RaiseHideWonScreen()
        {
            if (_hideWonScreen == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaiseHideWonScreen)} raised, but nothing picked it up");
                return;
            }
            _hideWonScreen.Invoke();
        }
        
        public static void RaisePlayButtonClickEvent()
        {
            if (_playButtonClick == null){
                LoggerService.LogWarning($"{nameof(RPSUIEvents)}::{nameof(RaisePlayButtonClickEvent)} raised, but nothing picked it up");
                return;
            }
            _playButtonClick.Invoke();
        }
    }
}