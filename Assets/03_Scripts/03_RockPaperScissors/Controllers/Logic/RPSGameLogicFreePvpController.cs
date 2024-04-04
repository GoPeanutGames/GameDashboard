using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSGameLogicFreePvpController: MonoBehaviour
	{
		//TODO: UI for connection?
		//TODO: on server connect -> server spawn connector
		//TODO: connector communicate mode / player type / player choice
		//TODO: server returns ok and returns opponent choice
		//TODO: server saves result, client shows everything for himself
		//TODO: server starts countdown, client is synced with it, client triggers choice
		//TODO: server returns choice, saves result, etc.
		//TODO: server triggers win / lose
		//TODO: server deactivates, disconnects, player removes everything related to server and starts again
		
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Sprite _questionMarkSprite;
		
		[SerializeField]
		private Sprite _rockSprite;
		
		[SerializeField]
		private Sprite _paperSprite;
		
		[SerializeField]
		private Sprite _scissorsSprite;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _round = 1;
		
		[SerializeField]
		private int _scorePlayer = 0;
		
		[SerializeField]
		private int _scoreEnemy = 0;
		
		[SerializeField]
		private RPSResultType _result;
		
		private void OnEnable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected += OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone += OnSelectedChoiceAnimationDone;
			RPSServerEvents.OpponentChoice += OnOpponentChoice;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone += OnBattleBgCloseAnimationDone;
			// RPSClientGameEvents.OnBattleAnimationDone += OnBattleAnimationDone;
			// RPSClientGameEvents.OnBattleBgOpenAnimationDone += StartNextRound;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected -= OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone -= OnSelectedChoiceAnimationDone;
			RPSServerEvents.OpponentChoice -= OnOpponentChoice;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone -= OnBattleBgCloseAnimationDone;
			// RPSClientGameEvents.OnBattleAnimationDone -= OnBattleAnimationDone;
			// RPSClientGameEvents.OnBattleBgOpenAnimationDone -= StartNextRound;
		}


		private void OnPlayChoiceSelectedEvent()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreeComputerController)}::{nameof(OnPlayChoiceSelectedEvent)}");
			RPSAudioEvents.RaiseFadeOutMusicEvent(0.5f);
			RPSClientGameEvents.RaiseDisablePlayerChoicesEvent();
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent(_round.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("PLAYER");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			ServerEvents.RaiseSpawnServerEvent();
		}
		
		private void OnSelectedChoiceAnimationDone()
		{
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("...");
			RPSUpperUIEvents.RaiseHideYourScoreEvent();
			RPSUpperUIEvents.RaiseHideEnemyScoreEvent();
			RPSUpperUIEvents.RaiseUpdateYourChoiceImageEvent(GetSpriteForChoice(RPSCurrentClientState.rpsChoiceType));
			RPSUpperUIEvents.RaiseUpdateEnemyChoiceImageEvent(_questionMarkSprite);
		}

		private void OnOpponentChoice(RPSChoiceType choiceType)
		{
			RPSCurrentEnemyState.rpsChoiceType = choiceType;
			RPSClientGameEvents.RaiseShowBattleEvent();
		}
		
		private void OnBattleBgCloseAnimationDone()
		{
			CalculateResult();
			RPSClientGameEvents.RaiseStartBattleAnimationEvent(_result);
		}
		
		private void CalculateResult()
		{
			switch (RPSCurrentEnemyState.rpsChoiceType){
				case RPSChoiceType.Paper:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							_result = RPSResultType.Draw;
							break;
						case RPSChoiceType.Rock:
							_result = RPSResultType.Lose;
							break;
						default:
							_result = RPSResultType.Win;
							break;
					}
					break;
				case RPSChoiceType.Rock:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							_result = RPSResultType.Win;
							break;
						case RPSChoiceType.Rock:
							_result = RPSResultType.Draw;
							break;
						default:
							_result = RPSResultType.Lose;
							break;
					}
					break;
				case RPSChoiceType.Scissors:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							_result = RPSResultType.Lose;
							break;
						case RPSChoiceType.Rock:
							_result = RPSResultType.Win;
							break;
						default:
							_result = RPSResultType.Draw;
							break;
					}
					break;
			}
		}
		
		private Sprite GetSpriteForChoice(RPSChoiceType choiceType)
		{
			switch (choiceType){
				case RPSChoiceType.Paper:
					return _paperSprite;
				case RPSChoiceType.Rock:
					return _rockSprite;
				case RPSChoiceType.Scissors:
					return _scissorsSprite;
			}
			return null;
		}
	}
}