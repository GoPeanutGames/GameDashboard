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
        private static readonly int Rock = Animator.StringToHash("Rock");
        private static readonly int Paper = Animator.StringToHash("Paper");
        private static readonly int Scissors = Animator.StringToHash("Scissors");
        private static readonly int MoveOut = Animator.StringToHash("MoveOut");
        private static readonly int WinFace = Animator.StringToHash("WinFace");
        private static readonly int LoseFace = Animator.StringToHash("LoseFace");

        private void Awake()
        {
            _wrapperAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation += OnPlayBotStartScreenAnimation;
            RPSBotEvents.OnPlayerShowPaper += OnPlayerShowPaper;
            RPSBotEvents.OnPlayerShowScissors += OnPlayerShowScissors;
            RPSBotEvents.OnPlayerShowRock += OnPlayerShowRock;
            RPSBotEvents.OnPlayerShowWinFace += OnPlayerShowWinFace;
            RPSBotEvents.OnPlayerShowLoseFace += OnPlayerShowLoseFace;
            RPSBotEvents.OnOpponentShowPaper += OnOpponentShowPaper;
            RPSBotEvents.OnOpponentShowScissors += OnOpponentShowScissors;
            RPSBotEvents.OnOpponentShowRock += OnOpponentShowRock;
            RPSBotEvents.OnOpponentShowWinFace += OnOpponentShowWinFace;
            RPSBotEvents.OnOpponentShowLoseFace += OnOpponentShowLoseFace;
            RPSBotEvents.OnHideBots += OnHideBots;
            RPSBotEvents.OnShowBots += OnShowBots;
            RPSBotEvents.OnResetBots += OnResetBots;
            RPSBotEvents.OnMoveOutBots += OnMoveOutBots;
        }

        private void OnDisable()
        {
            RPSBotEvents.OnPlayStartScreenBotAnimation -= OnPlayBotStartScreenAnimation;
            RPSBotEvents.OnPlayerShowPaper -= OnPlayerShowPaper;
            RPSBotEvents.OnPlayerShowScissors -= OnPlayerShowScissors;
            RPSBotEvents.OnPlayerShowRock -= OnPlayerShowRock;
            RPSBotEvents.OnPlayerShowWinFace -= OnPlayerShowWinFace;
            RPSBotEvents.OnPlayerShowLoseFace -= OnPlayerShowLoseFace;
            RPSBotEvents.OnOpponentShowPaper -= OnOpponentShowPaper;
            RPSBotEvents.OnOpponentShowScissors -= OnOpponentShowScissors;
            RPSBotEvents.OnOpponentShowRock -= OnOpponentShowRock;
            RPSBotEvents.OnOpponentShowWinFace -= OnOpponentShowWinFace;
            RPSBotEvents.OnOpponentShowLoseFace -= OnOpponentShowLoseFace;
            RPSBotEvents.OnHideBots -= OnHideBots;
            RPSBotEvents.OnShowBots -= OnShowBots;
            RPSBotEvents.OnResetBots -= OnResetBots;
            RPSBotEvents.OnMoveOutBots -= OnMoveOutBots;
        }

        private void OnResetBots()
        {
            _playerAnimator.SetBool(StartScreen, false);
            _opponentAnimator.SetBool(StartScreen, false);
            _playerAnimator.SetBool(Rock, false);
            _opponentAnimator.SetBool(Rock, false);
            _playerAnimator.SetBool(Scissors, false);
            _opponentAnimator.SetBool(Scissors, false);
            _playerAnimator.SetBool(Paper, false);
            _opponentAnimator.SetBool(Paper, false);
            _playerAnimator.SetBool(WinFace, false);
            _opponentAnimator.SetBool(WinFace, false);
            _playerAnimator.SetBool(LoseFace, false);
            _opponentAnimator.SetBool(LoseFace, false);
        }
        
        private void OnPlayBotStartScreenAnimation()
        {
            LoggerService.LogInfo($"{nameof(RPSBotController)}::{nameof(OnPlayBotStartScreenAnimation)}");
            OnShowBots();
            _playerAnimator.SetBool(StartScreen, true);
            _opponentAnimator.SetBool(StartScreen, true);
        }

        private void OnPlayerShowRock()
        {
            _playerAnimator.SetBool(Rock, true);
        }

        private void OnPlayerShowPaper()
        {
            _playerAnimator.SetBool(Paper, true);
        }

        private void OnPlayerShowScissors()
        {
            _playerAnimator.SetBool(Scissors, true);   
        }

        private void OnPlayerShowWinFace()
        {
            _playerAnimator.SetBool(WinFace, true);
        }

        private void OnPlayerShowLoseFace()
        {
            _playerAnimator.SetBool(LoseFace, true);
        }
        
        private void OnOpponentShowRock()
        {
            _opponentAnimator.SetBool(Rock, true);
        }

        private void OnOpponentShowPaper()
        {
            _opponentAnimator.SetBool(Paper, true);
        }

        private void OnOpponentShowScissors()
        {
            _opponentAnimator.SetBool(Scissors, true);   
        }

        private void OnOpponentShowWinFace()
        {
            _opponentAnimator.SetBool(WinFace, true);
        }

        private void OnOpponentShowLoseFace()
        {
            _opponentAnimator.SetBool(LoseFace, true);
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

        private void OnMoveOutBots()
        {
            _wrapperAnimator.SetTrigger(MoveOut);
        }
    }
}