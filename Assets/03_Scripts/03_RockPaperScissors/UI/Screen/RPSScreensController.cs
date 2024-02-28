﻿using PeanutDashboard._03_RockPaperScissors.Events;
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
        
        private void OnEnable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen += OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen += OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen += OnShowGameChooseOptionScreen;
            RPSUIEvents.OnShowStartScreen += OnShowStartScreen;
            RPSUIEvents.OnHideWonScreen += OnHideWonScreen;
            RPSClientGameEvents.OnShowBattle += OnShowBattle;
            RPSClientGameEvents.OnBattleBgOpenAnimationDone += OnHideBattleBgDone;
            RPSClientGameEvents.OnYouWonGame += OnShowWonScreen;
        }

        private void OnDisable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen -= OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen -= OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen -= OnShowGameChooseOptionScreen;
            RPSUIEvents.OnShowStartScreen -= OnShowStartScreen;
            RPSUIEvents.OnHideWonScreen -= OnHideWonScreen;
            RPSClientGameEvents.OnShowBattle -= OnShowBattle;
            RPSClientGameEvents.OnBattleBgOpenAnimationDone -= OnHideBattleBgDone;
            RPSClientGameEvents.OnYouWonGame -= OnShowWonScreen;
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
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowChooseModeScreen)}");
            DisableAllScreens();
            _startScreen.Activate();   
        }
        
        private void OnShowChooseModeScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowChooseModeScreen)}");
            DisableAllScreens();
            _chooseModeScreen.Activate();
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
        }

        private void OnShowBattle()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowBattle)}");
            _gameBattleScreen.Activate();
        }
        
        private void OnHideBattleBgDone()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnHideBattleBgDone)}");
            _gameBattleScreen.Deactivate();
        }

        private void OnShowWonScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowWonScreen)}");
            _gameBattleScreen.Deactivate();
            _wonScreen.Activate();
        }

        private void OnHideWonScreen()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnHideWonScreen)}");
            _wonScreen.Deactivate();
        }
    }
}