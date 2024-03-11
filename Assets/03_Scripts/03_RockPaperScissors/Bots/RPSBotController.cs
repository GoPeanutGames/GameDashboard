using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
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

        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Animator _wrapperAnimator;

        private static readonly int StartScreen = Animator.StringToHash("StartScreen");
        private static readonly int MoveIn = Animator.StringToHash("MoveIn");

        private void Awake()
        {
            _wrapperAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation += OnPlayBotStartScreenAnimation;
            RPSBotEvents.OnHideBots += OnHideBots;
            RPSBotEvents.OnShowBots += OnShowBots;
        }

        private void OnDisable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation -= OnPlayBotStartScreenAnimation;
            RPSBotEvents.OnHideBots -= OnHideBots;
            RPSBotEvents.OnShowBots -= OnShowBots;
        }

        private void OnPlayBotStartScreenAnimation()
        {
            LoggerService.LogInfo($"{nameof(RPSBotController)}::{nameof(OnPlayBotStartScreenAnimation)}");
            _playerAnimator.SetBool(StartScreen, true);
            _opponentAnimator.SetBool(StartScreen, true);
        }

        private void OnHideBots()
        {
            _playerAnimator.gameObject.Deactivate();
            _opponentAnimator.gameObject.Deactivate();
        }

        private void OnShowBots()
        {
            _playerAnimator.gameObject.Activate();
            _opponentAnimator.gameObject.Activate();
            _wrapperAnimator.SetTrigger(MoveIn);
        }
    }
}