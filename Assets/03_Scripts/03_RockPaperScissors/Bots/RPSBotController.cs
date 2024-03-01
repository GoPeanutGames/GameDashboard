using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Bots
{
    public class RPSBotController: MonoBehaviour
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
        }

        private void OnDisable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation -= OnPlayBotStartScreenAnimation;
        }

        private void OnPlayBotStartScreenAnimation()
        {
            LoggerService.LogInfo($"{nameof(RPSBotController)}::{nameof(OnPlayBotStartScreenAnimation)}");
            _playerAnimator.SetBool(StartScreen, true);
        }
    }
}