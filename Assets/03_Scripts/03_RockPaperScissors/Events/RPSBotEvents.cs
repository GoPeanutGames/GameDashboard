using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
    public static class RPSBotEvents
    {
        private static UnityAction _playStartScreenBotAnimation;
        private static UnityAction _stopStartScreenBotAnimation;
        private static UnityAction _playerShowRock;
        private static UnityAction _playerShowScissors;
        private static UnityAction _playerShowPaper;
        private static UnityAction _playerShowWinFace;
        private static UnityAction _playerShowLoseFace;
        private static UnityAction _opponentShowRock;
        private static UnityAction _opponentShowScissors;
        private static UnityAction _opponentShowPaper;
        private static UnityAction _opponentShowWinFace;
        private static UnityAction _opponentShowLoseFace;
        private static UnityAction _hideBots;
        private static UnityAction _showBots;
        private static UnityAction _moveOutBots;
        private static UnityAction _resetBots;
        
        public static event UnityAction OnPlayStartScreenBotAnimation
        {
            add => _playStartScreenBotAnimation += value;
            remove => _playStartScreenBotAnimation -= value;
        }
        
        public static event UnityAction OnStopStartScreenBotAnimation
        {
            add => _stopStartScreenBotAnimation += value;
            remove => _stopStartScreenBotAnimation -= value;
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
        
        public static event UnityAction OnMoveOutBots
        {
            add => _moveOutBots += value;
            remove => _moveOutBots -= value;
        }
        
        public static event UnityAction OnResetBots
        {
            add => _resetBots += value;
            remove => _resetBots -= value;
        }

        public static event UnityAction OnPlayerShowRock
        {
            add => _playerShowRock += value;
            remove => _playerShowRock -= value;
        }
        
        public static event UnityAction OnPlayerShowScissors
        {
            add => _playerShowScissors += value;
            remove => _playerShowScissors -= value;
        }
        
        public static event UnityAction OnPlayerShowPaper
        {
            add => _playerShowPaper += value;
            remove => _playerShowPaper -= value;
        }
        
        public static event UnityAction OnPlayerShowWinFace
        {
            add => _playerShowWinFace += value;
            remove => _playerShowWinFace -= value;
        }
        
        public static event UnityAction OnPlayerShowLoseFace
        {
            add => _playerShowLoseFace += value;
            remove => _playerShowLoseFace -= value;
        }

        public static event UnityAction OnOpponentShowRock
        {
            add => _opponentShowRock += value;
            remove => _opponentShowRock -= value;
        }
        
        public static event UnityAction OnOpponentShowScissors
        {
            add => _opponentShowScissors += value;
            remove => _opponentShowScissors -= value;
        }
        
        public static event UnityAction OnOpponentShowPaper
        {
            add => _opponentShowPaper += value;
            remove => _opponentShowPaper -= value;
        }
        
        public static event UnityAction OnOpponentShowWinFace
        {
            add => _opponentShowWinFace += value;
            remove => _opponentShowWinFace -= value;
        }
        
        public static event UnityAction OnOpponentShowLoseFace
        {
            add => _opponentShowLoseFace += value;
            remove => _opponentShowLoseFace -= value;
        }

        public static void RaisePlayStartScreenBotAnimationEvent()
        {
            if (_playStartScreenBotAnimation == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayStartScreenBotAnimationEvent)} raised, but nothing picked it up");
                return;
            }
            _playStartScreenBotAnimation.Invoke();
        }
        
        public static void RaisePlayStopScreenBotAnimationEvent()
        {
            if (_stopStartScreenBotAnimation == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayStopScreenBotAnimationEvent)} raised, but nothing picked it up");
                return;
            }
            _stopStartScreenBotAnimation.Invoke();
        }

        public static void RaiseHideBotsEvent()
        {
            if (_hideBots == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseHideBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _hideBots.Invoke();
        }

        public static void RaiseMoveOutBotsEvent()
        {
            if (_moveOutBots == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseMoveOutBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _moveOutBots.Invoke();
        }
        
        public static void RaiseShowBotsEvent()
        {
            if (_showBots == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseShowBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _showBots.Invoke();
        }
        
        public static void RaiseResetBotsEvent()
        {
            if (_resetBots == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseResetBotsEvent)} raised, but nothing picked it up");
                return;
            }
            _resetBots.Invoke();
        }
        
        public static void RaisePlayerShowRockEvent()
        {
            if (_playerShowRock == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayerShowRockEvent)} raised, but nothing picked it up");
                return;
            }
            _playerShowRock.Invoke();
        }
        
        public static void RaisePlayerShowPaperEvent()
        {
            if (_playerShowPaper == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayerShowPaperEvent)} raised, but nothing picked it up");
                return;
            }
            _playerShowPaper.Invoke();
        }
        
        public static void RaisePlayerShowScissorsEvent()
        {
            if (_playerShowScissors == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayerShowScissorsEvent)} raised, but nothing picked it up");
                return;
            }
            _playerShowScissors.Invoke();
        }
        
        public static void RaisePlayerShowWinFaceEvent()
        {
            if (_playerShowWinFace == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayerShowWinFaceEvent)} raised, but nothing picked it up");
                return;
            }
            _playerShowWinFace.Invoke();
        }
        
        public static void RaisePlayerShowLoseFaceEvent()
        {
            if (_playerShowLoseFace == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaisePlayerShowLoseFaceEvent)} raised, but nothing picked it up");
                return;
            }
            _playerShowLoseFace.Invoke();
        }
        
        public static void RaiseOpponentShowRockEvent()
        {
            if (_opponentShowRock == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseOpponentShowRockEvent)} raised, but nothing picked it up");
                return;
            }
            _opponentShowRock.Invoke();
        }
        
        public static void RaiseOpponentShowPaperEvent()
        {
            if (_opponentShowPaper == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseOpponentShowPaperEvent)} raised, but nothing picked it up");
                return;
            }
            _opponentShowPaper.Invoke();
        }
        
        public static void RaiseOpponentShowScissorsEvent()
        {
            if (_opponentShowScissors == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseOpponentShowScissorsEvent)} raised, but nothing picked it up");
                return;
            }
            _opponentShowScissors.Invoke();
        }
        
        public static void RaiseOpponentShowWinFaceEvent()
        {
            if (_opponentShowWinFace == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseOpponentShowWinFaceEvent)} raised, but nothing picked it up");
                return;
            }
            _opponentShowWinFace.Invoke();
        }
        
        public static void RaiseOpponentShowLoseFaceEvent()
        {
            if (_opponentShowLoseFace == null){
                LoggerService.LogWarning($"{nameof(RPSBotEvents)}::{nameof(RaiseOpponentShowLoseFaceEvent)} raised, but nothing picked it up");
                return;
            }
            _opponentShowLoseFace.Invoke();
        }
    }
}