using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
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
		
		[SerializeField]
		private AudioClip _winRound;
		
		[SerializeField]
		private AudioClip _winGame;
		
		[SerializeField]
		private AudioClip _loseGame;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _round = 1;
		
		[SerializeField]
		private int _scorePlayer = 0;
		
		[SerializeField]
		private int _scoreEnemy = 0;
		
		[SerializeField]
		private Sprite _winIndicator;
		
		[SerializeField]
		private Sprite _loseIndicator;
		
		[SerializeField]
		private RPSResultType _result;

		[SerializeField]
		private bool _firstRoundDone = false;
		
		[SerializeField]
		private bool _serverWon = false;
		
		[SerializeField]
		private bool _serverEndGame = false;
		
		private void OnEnable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected += OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone += OnSelectedChoiceAnimationDone;
			RPSServerEvents.OpponentChoice += OnOpponentChoice;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone += OnBattleBgCloseAnimationDone;
			RPSClientGameEvents.OnBattleAnimationDone += OnBattleAnimationDone;
			RPSClientGameEvents.OnBattleBgOpenAnimationDone += StartNextRound;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected -= OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone -= OnSelectedChoiceAnimationDone;
			RPSServerEvents.OpponentChoice -= OnOpponentChoice;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone -= OnBattleBgCloseAnimationDone;
			RPSClientGameEvents.OnBattleAnimationDone -= OnBattleAnimationDone;
			RPSClientGameEvents.OnBattleBgOpenAnimationDone -= StartNextRound;
		}


		private void OnPlayChoiceSelectedEvent()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(OnPlayChoiceSelectedEvent)}");
			RPSAudioEvents.RaiseFadeOutMusicEvent(0.5f);
			RPSClientGameEvents.RaiseDisablePlayerChoicesEvent();
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent(_round.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("PLAYER");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			if (!_firstRoundDone){
				_firstRoundDone = true;
				ServerEvents.RaiseSpawnServerEvent();
			}
			else{
				RPSServerEvents.RaiseSendChoiceToServerEvent();
			}
		}
		
		private void OnSelectedChoiceAnimationDone()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(OnSelectedChoiceAnimationDone)}");
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("...");
			RPSUpperUIEvents.RaiseHideYourScoreEvent();
			RPSUpperUIEvents.RaiseHideEnemyScoreEvent();
			RPSUpperUIEvents.RaiseUpdateYourChoiceImageEvent(GetSpriteForChoice(RPSCurrentClientState.rpsChoiceType));
			RPSUpperUIEvents.RaiseUpdateEnemyChoiceImageEvent(_questionMarkSprite);
		}

		private void OnOpponentChoice(RPSChoiceType choiceType, bool wonGame, bool endGame)
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(OnOpponentChoice)} - {choiceType}, {wonGame}, {endGame}");
			_serverWon = wonGame;
			_serverEndGame = endGame;
			RPSCurrentEnemyState.rpsChoiceType = choiceType;
			RPSClientGameEvents.RaiseShowBattleEvent();
		}
		
		private void OnBattleBgCloseAnimationDone()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(OnBattleBgCloseAnimationDone)}");
			CalculateResult();
			RPSClientGameEvents.RaiseStartBattleAnimationEvent(_result);
		}
		
		private void OnBattleAnimationDone()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(OnBattleAnimationDone)}");
			RPSUpperUIEvents.RaiseUpdateEnemyChoiceImageEvent(GetSpriteForChoice(RPSCurrentEnemyState.rpsChoiceType));
			switch (_result){
				case RPSResultType.Win:
					Won();
					break;
				case RPSResultType.Lose:
					Lost();
					break;
				case RPSResultType.Draw:
					Draw();
					break;
			}
		}
		
		private void Won()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(Won)}");
			RPSUpperUIEvents.RaiseUpdateUpperIndicatorEvent(_winIndicator);
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("WIN");
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			_scorePlayer++;
			_round++;
			RPSLifeGameEvents.RaiseBurstHeartEvent(RPSUserType.Opponent);
			if (_serverEndGame){
				LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(Won)} - server said won");
				RPSAudioEvents.RaisePlaySfxEvent(_winGame, 1f);
				RPSClientGameEvents.RaiseYouWonGameEvent();
                NetworkManager.Singleton.Shutdown();
                Destroy(NetworkManager.Singleton.gameObject);
				Destroy(this.gameObject);
			}
			else{
				RPSAudioEvents.RaisePlaySfxEvent(_winRound, 1f);
				RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
			}
		}

		private void Lost()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(Lost)}");
			RPSUpperUIEvents.RaiseUpdateUpperIndicatorEvent(_loseIndicator);
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("LOSE");
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			_scoreEnemy++;
			_round++;
			RPSLifeGameEvents.RaiseBurstHeartEvent(RPSUserType.Player);
			if (_serverEndGame){
				LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(Won)} - server said lose");
				RPSAudioEvents.RaisePlaySfxEvent(_loseGame, 1f);
				RPSClientGameEvents.RaiseYouLostGameEvent();
                NetworkManager.Singleton.Shutdown();
                Destroy(NetworkManager.Singleton.gameObject);
				Destroy(this.gameObject);
			}
			else{
				RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
			}
		}

		private void Draw()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(Draw)}");
			_round++;
			RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
		}
		
		
		private void StartNextRound()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreePvpController)}::{nameof(StartNextRound)}");
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("Player");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			RPSUpperUIEvents.RaiseShowEnemyScoreEvent();
			RPSUpperUIEvents.RaiseShowYourScoreEvent();
			RPSUpperUIEvents.RaiseHideIndicatorEvent();
			RPSClientGameEvents.RaiseEnablePlayerChoicesEvent();
			RPSTimerEvents.RaiseStartTimerEvent(5f);
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