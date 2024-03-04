using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSGameLogicFreeComputerController : MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _round = 1;
		
		[SerializeField]
		private int _scorePlayer = 0;
		
		[SerializeField]
		private int _scoreEnemy = 0;
		
		[SerializeField]
		private Sprite _rockSprite;
		
		[SerializeField]
		private Sprite _paperSprite;
		
		[SerializeField]
		private Sprite _scissorsSprite;
		
		[SerializeField]
		private Sprite _questionMarkSprite;
		
		[SerializeField]
		private Sprite _winIndicator;
		
		[SerializeField]
		private Sprite _loseIndicator;
        
		
		private void OnEnable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected += OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone += OnSelectedChoiceAnimationDone;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone += OnBattleBgCloseAnimationDone;
			RPSClientGameEvents.OnBattleAnimationDone += OnBattleAnimationDone;
			RPSClientGameEvents.OnBattleBgOpenAnimationDone += StartNextRound;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected -= OnPlayChoiceSelectedEvent;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone -= OnSelectedChoiceAnimationDone;
			RPSClientGameEvents.OnBattleBgCloseAnimationDone -= OnBattleBgCloseAnimationDone;
			RPSClientGameEvents.OnBattleAnimationDone -= OnBattleAnimationDone;
			RPSClientGameEvents.OnBattleBgOpenAnimationDone -= StartNextRound;
		}

		private void OnPlayChoiceSelectedEvent()
		{
			LoggerService.LogInfo($"{nameof(RPSGameLogicFreeComputerController)}::{nameof(OnPlayChoiceSelectedEvent)}");
			RPSClientGameEvents.RaiseDisablePlayerChoicesEvent();
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent(_round.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("AI");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
		}

		private void OnSelectedChoiceAnimationDone()
		{
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("...");
			RPSUpperUIEvents.RaiseHideYourScoreEvent();
			RPSUpperUIEvents.RaiseHideEnemyScoreEvent();
			RPSUpperUIEvents.RaiseUpdateYourChoiceImageEvent(GetSpriteForChoice(RPSCurrentClientState.rpsChoiceType));
			RPSUpperUIEvents.RaiseUpdateEnemyChoiceImageEvent(_questionMarkSprite);
			RPSClientGameEvents.RaiseShowBattleEvent();
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

		private void OnBattleBgCloseAnimationDone()
		{
			RPSCurrentEnemyState.rpsChoiceType = (RPSChoiceType)Random.Range(0, 3);
			RPSClientGameEvents.RaiseStartBattleAnimationEvent();
		}

		private void OnBattleAnimationDone()
		{
			RPSUpperUIEvents.RaiseUpdateEnemyChoiceImageEvent(GetSpriteForChoice(RPSCurrentEnemyState.rpsChoiceType));
			switch (RPSCurrentEnemyState.rpsChoiceType){
				case RPSChoiceType.Paper:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							Draw();
							break;
						case RPSChoiceType.Rock:
							Lost();
							break;
						default:
							Won();
							break;
					}
					break;
				case RPSChoiceType.Rock:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							Won();
							break;
						case RPSChoiceType.Rock:
							Draw();
							break;
						default:
							Lost();
							break;
					}
					break;
				case RPSChoiceType.Scissors:
					switch (RPSCurrentClientState.rpsChoiceType){
						case RPSChoiceType.Paper:
							Lost();
							break;
						case RPSChoiceType.Rock:
							Won();
							break;
						default:
							Draw();
							break;
					}
					break;
			}
			//TODO: enable timer - for timeout - automatic lose
			//TODO: get the robot animations in battle
		}

		private void Won()
		{
			RPSUpperUIEvents.RaiseUpdateUpperIndicatorEvent(_winIndicator);
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("WIN");
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			_scorePlayer++;
			_round++;
			RPSLifeGameEvents.RaiseBurstHeartEvent(RPSUserType.Opponent);
			if (_scorePlayer == 3){
				RPSClientGameEvents.RaiseYouWonGameEvent();
				Destroy(this.gameObject);
			}
			else{
				RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
			}
		}

		private void Lost()
		{
			RPSUpperUIEvents.RaiseUpdateUpperIndicatorEvent(_loseIndicator);
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent("LOSE");
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("");
			_scoreEnemy++;
			_round++;
			RPSLifeGameEvents.RaiseBurstHeartEvent(RPSUserType.Player);
			if (_scoreEnemy == 3){
				RPSClientGameEvents.RaiseYouLostGameEvent();
				Destroy(this.gameObject);
			}
			else{
				RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
			}
		}

		private void Draw()
		{
			_round++;
            RPSClientGameEvents.RaiseStartBattleBgOpenAnimationEvent();
		}

		private void StartNextRound()
		{
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("ROUND");
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent(_round.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("AI");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			RPSUpperUIEvents.RaiseShowEnemyScoreEvent();
			RPSUpperUIEvents.RaiseShowYourScoreEvent();
			RPSUpperUIEvents.RaiseHideIndicatorEvent();
			RPSClientGameEvents.RaiseEnablePlayerChoicesEvent();
		}
	}
}