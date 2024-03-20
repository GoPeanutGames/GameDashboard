using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
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

		private void OnStartBattleAnimation(RPSResultType rpsResultType)
		{
			LoggerService.LogInfo($"{nameof(RPSGameBattleScreenController)}::{nameof(OnStartBattleAnimation)}");
			RPSChoiceType playerChoice = RPSCurrentClientState.rpsChoiceType;
			RPSChoiceType enemyChoice = RPSCurrentEnemyState.rpsChoiceType;
			RPSBotEvents.RaiseShowBotsEvent();
			SetPlayerRobotChoice(playerChoice);
			SetPlayerRobotHead(rpsResultType);
			SetOpponentRobotChoice(enemyChoice);
			SetOpponentRobotHead(rpsResultType);
			Invoke(nameof(MoveOut), 1f);
		}

		private void SetPlayerRobotChoice(RPSChoiceType rpsChoiceType)
		{
			switch (rpsChoiceType){
				case RPSChoiceType.Rock:
					RPSBotEvents.RaisePlayerShowRockEvent();
					break;
				case RPSChoiceType.Paper:
					RPSBotEvents.RaisePlayerShowPaperEvent();
					break;
				case RPSChoiceType.Scissors:
					RPSBotEvents.RaisePlayerShowScissorsEvent();
					break;
			}
		}

		private void SetPlayerRobotHead(RPSResultType rpsResultType)
		{
			switch (rpsResultType){
				case RPSResultType.Lose:
					RPSBotEvents.RaisePlayerShowLoseFaceEvent();
					break;
				case RPSResultType.Win:
					RPSBotEvents.RaisePlayerShowWinFaceEvent();
					break;
			}
		}

		private void SetOpponentRobotChoice(RPSChoiceType rpsChoiceType)
		{
			switch (rpsChoiceType){
				case RPSChoiceType.Rock:
					RPSBotEvents.RaiseOpponentShowRockEvent();
					break;
				case RPSChoiceType.Paper:
					RPSBotEvents.RaiseOpponentShowPaperEvent();
					break;
				case RPSChoiceType.Scissors:
					RPSBotEvents.RaiseOpponentShowScissorsEvent();
					break;
			}
		}

		private void SetOpponentRobotHead(RPSResultType rpsResultType)
		{
			switch (rpsResultType){
				case RPSResultType.Lose:
					RPSBotEvents.RaiseOpponentShowWinFaceEvent();
					break;
				case RPSResultType.Win:
					RPSBotEvents.RaiseOpponentShowLoseFaceEvent();
					break;
			}
		}

		private void MoveOut()
		{
			RPSBotEvents.RaiseMoveOutBotsEvent();
			Invoke(nameof(End), 1f);
		}
		
		private void End()
		{
			RPSClientGameEvents.RaiseBattleAnimationDoneEvent();
		}
	}
}