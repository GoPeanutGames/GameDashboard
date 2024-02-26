using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSGameLogicFreeComputerController : MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _round = 0;
		
		[SerializeField]
		private int _scorePlayer = 0;
		
		[SerializeField]
		private int _scoreEnemy = 0;
        
		
		private void OnEnable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected += OnPlayChoiceSelectedEvent;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected -= OnPlayChoiceSelectedEvent;
		}

		private void OnPlayChoiceSelectedEvent()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreeComputerController)}::{nameof(OnPlayChoiceSelectedEvent)}");
			RPSClientGameEvents.RaiseDisablePlayerChoicesEvent();
			_round++;
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent(_round.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("AI");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			//TODO: flow:
			//TODO: - disable play button & toggles
			//TODO: - update upper UI
			//TODO: - show choice animation small to big ->
			//TODO: - on choice animation done -> wait 0.2sec and trigger game screen
			//TODO: - show game battle (bots), when their animation is done, show win / lose and burst appropriate bubble
			//TODO: - hide game battle, enable play button & toggles
			//TODO: - enable timer
		}
	}
}