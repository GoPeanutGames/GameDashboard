using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Bots
{
    public class RPSStartScreenBotController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private Animator _playerAnimator;
        
        [SerializeField]
        private Animator _opponentAnimator;

        private static readonly int StartScreen = Animator.StringToHash("StartScreen");

        private void OnEnable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation += OnPlayBotStartScreenAnimation;
            RPSUIEvents.OnShowStartScreen += OnShowStartScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen += OnShowChooseOptionsScreen;
        }

        private void OnDisable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation -= OnPlayBotStartScreenAnimation;
            RPSUIEvents.OnShowStartScreen -= OnShowStartScreen;
            RPSUIEvents.OnShowGameChooseOptionScreen -= OnShowChooseOptionsScreen;
        }

        private void OnShowChooseOptionsScreen()
        {
            _playerAnimator.gameObject.Deactivate();
            _opponentAnimator.gameObject.Deactivate();
        }

        private void OnShowStartScreen()
        {
            _playerAnimator.gameObject.Activate();
            _opponentAnimator.gameObject.Activate();
        }
        
        private void OnPlayBotStartScreenAnimation()
        {
            LoggerService.LogInfo($"{nameof(RPSStartScreenBotController)}::{nameof(OnPlayBotStartScreenAnimation)}");
            _playerAnimator.SetBool(StartScreen, true);
            _opponentAnimator.SetBool(StartScreen, true);
        }
    }
}