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
        private GameObject _chooseModeScreen;
        
        [SerializeField]
        private GameObject _chooseOpponentScreen;
        
        [SerializeField]
        private GameObject _gameChooseOptionsScreen;
        
        [SerializeField]
        private GameObject _gameBattleScreen;
        
        private void OnEnable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen += OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen += OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen += OnShowGameChooseOptionScreen;
            RPSClientGameEvents.OnShowBattle += OnShowBattle;
        }

        private void OnDisable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen -= OnShowChooseOpponentScreen;
            RPSUIEvents.OnShowChooseModeScreen -= OnShowChooseModeScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen -= OnShowGameChooseOptionScreen;
            RPSClientGameEvents.OnShowBattle -= OnShowBattle;
        }

        private void DisableAllScreens()
        {
            _chooseModeScreen.Deactivate();
            _chooseOpponentScreen.Deactivate();
            _gameChooseOptionsScreen.Deactivate();
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
        }

        private void OnShowBattle()
        {
            LoggerService.LogInfo($"{nameof(RPSScreensController)}::{nameof(OnShowBattle)}");
            _gameBattleScreen.Activate();
        }
    }
}