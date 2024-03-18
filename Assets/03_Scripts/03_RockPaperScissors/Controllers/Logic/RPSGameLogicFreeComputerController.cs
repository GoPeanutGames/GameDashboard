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

		[SerializeField]
		private RPSResultType _result;
        
		
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
		
		private void OnBattleAnimationDone()
		{
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
			RPSUpperUIEvents.RaiseUpdateEnemyNameTextEvent("AI");
			RPSUpperUIEvents.RaiseUpdateYourScoreTextEvent(_scorePlayer.ToString());
			RPSUpperUIEvents.RaiseUpdateEnemyScoreTextEvent(_scoreEnemy.ToString());
			RPSUpperUIEvents.RaiseShowEnemyScoreEvent();
			RPSUpperUIEvents.RaiseShowYourScoreEvent();
			RPSUpperUIEvents.RaiseHideIndicatorEvent();
			RPSClientGameEvents.RaiseEnablePlayerChoicesEvent();
			RPSTimerEvents.RaiseStartTimerEvent(5f);
		}
	}
}