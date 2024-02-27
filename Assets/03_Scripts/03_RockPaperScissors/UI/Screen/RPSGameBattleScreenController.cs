using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSGameBattleScreenController: MonoBehaviour
	{
		private void OnEnable()
		{
			RPSClientGameEvents.OnStartBattleAnimation += OnStartBattleAnimation;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnStartBattleAnimation -= OnStartBattleAnimation;
		}

		private void OnStartBattleAnimation()
		{
			LoggerService.LogInfo($"{nameof(RPSGameBattleScreenController)}::{nameof(OnStartBattleAnimation)}");
			//TODO: launch animation
			Invoke(nameof(End), 1f);
		}

		private void End()
		{
			RPSClientGameEvents.RaiseBattleAnimationDoneEvent();
		}
	}
}