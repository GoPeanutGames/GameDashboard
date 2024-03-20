using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
    public class RPSScreensController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _startScreen;
        
        [SerializeField]
        private GameObject _chooseModeScreen;
        
        [SerializeField]
        private GameObject _chooseOpponentScreen;
        
        [SerializeField]
        private GameObject _gameChooseOptionsScreen;
        
        [SerializeField]
        private GameObject _gameBattleScreen;
        
        [SerializeField]
        private GameObject _wonScreen;
        
        [SerializeField]
        private GameObject _loseScreen;
        
        [SerializeField]
        private AudioClip _startMusic;
        
        [SerializeField]
        private AudioClip _chooseModeMusic;
        
        [SerializeField]
        private AudioClip _battleMusic;
        
        private void OnEnable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen += OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen += OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen += OnShowGameChooseOptionScreen;
            RPSUIEvents.OnShowStartScreen += OnShowStartScreen;
            RPSUIEvents.OnHideResultScreens += OnHideResultScreens;
            RPSClientGameEvents.OnShowBattle += OnShowBattle;
            RPSClientGameEvents.OnBattleBgOpenAnimationDone += OnHideBattleBgDone;
            RPSClientGameEvents.OnYouWonGame += OnShowWonScreen;
            RPSClientGameEvents.OnYouLostGame += OnShowLoseScreen;
        }

        private void OnDisable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen -= OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen -= OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen -= OnShowGameChooseOptionScreen;
            RPSUIEvents.OnShowStartScreen -= OnShowStartScreen;
            RPSUIEvents.OnHideResultScreens -= OnHideResultScreens;
            RPSClientGameEvents.OnShowBattle -= OnShowBattle;
            RPSClientGameEvents.OnBattleBgOpenAnimationDone -= OnHideBattleBgDone;
            RPSClientGameEvents.OnYouWonGame -= OnShowWonScreen;
            RPSClientGameEvents.OnYouLostGame -= OnShowLoseScreen;
        }

        private void Start()
        {
            OnShowStartScreen();
        }

        private void DisableAllScreens()
        {
            _chooseModeScreen.Deactivate();
            _chooseOpponentScreen.Deactivate();
            _gameChooseOptionsScreen.Deactivate();
            _startScreen.Deactivate();
        }
        
        private void OnShowStartScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowStartScreen)}");
            DisableAllScreens();
            _startScreen.Activate();
            RPSBotEvents.RaisePlayStartScreenBotAnimationEvent();
            RPSAudioEvents.RaiseFadeInMusicEvent(_startMusic);
        }
        
        private void OnShowChooseModeScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowChooseModeScreen)}");
            DisableAllScreens();
            _chooseModeScreen.Activate();
            RPSAudioEvents.RaiseFadeInMusicEvent(_chooseModeMusic);
        }
        
        private void OnShowChooseOpponentScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowChooseOpponentScreen)}");
            DisableAllScreens();
            _chooseOpponentScreen.Activate();
        }

        private void OnShowGameChooseOptionScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowGameChooseOptionScreen)}");
            DisableAllScreens();
            _gameChooseOptionsScreen.Activate();
            RPSUpperUIEvents.RaiseShowYourScoreEvent();
            RPSUpperUIEvents.RaiseShowEnemyScoreEvent();
            RPSClientGameEvents.RaiseEnablePlayerChoicesEvent();
            RPSUpperUIEvents.RaiseHideIndicatorEvent();
            RPSUpperUIEvents.RaiseShowYourScoreEvent();
            RPSUpperUIEvents.RaiseShowEnemyScoreEvent();
            RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent("_");
            RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent("_");
            RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
            RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("-");
            RPSBotEvents.RaiseHideBotsEvent();
        }

        private void OnShowBattle()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowBattle)}");
            _gameBattleScreen.Activate();
            RPSBotEvents.RaiseResetBotsEvent();
            RPSAudioEvents.RaisePlaySfxEvent(_battleMusic, 1f);
        }
        
        private void OnHideBattleBgDone()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnHideBattleBgDone)}");
            _gameBattleScreen.Deactivate();
            RPSBotEvents.RaiseResetBotsEvent();
        }

        private void OnShowWonScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowWonScreen)}");
            _gameBattleScreen.Deactivate();
            _wonScreen.Activate();
        }

        private void OnHideResultScreens()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnHideResultScreens)}");
            _wonScreen.Deactivate();
            _loseScreen.Deactivate();
        }

        private void OnShowLoseScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowLoseScreen)}");
            _gameBattleScreen.Deactivate();
            _loseScreen.Activate();
        }
    }
}