using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSClientGameEvents
	{
		private static UnityAction _playChoiceSelected;
		private static UnityAction _disablePlayerChoices;
		private static UnityAction _enablePlayerChoices;
		private static UnityAction _selectedChoiceAnimationDone;
		private static UnityAction _showBattle;
		private static UnityAction _battleBgCloseAnimationDone;
		private static UnityAction<RPSResultType> _startBattleAnimation;
		private static UnityAction _battleAnimationDone;
		private static UnityAction _startBattleBgOpenAnimation;
		private static UnityAction _battleBgOpenAnimationDone;
		private static UnityAction _youWonGame;
		private static UnityAction _youLostGame;
        
		public static event UnityAction OnPlayChoiceSelected
		{
			add => _playChoiceSelected += value;
			remove => _playChoiceSelected -= value;
		}
		
		public static event UnityAction OnDisablePlayerChoices
		{
			add => _disablePlayerChoices += value;
			remove => _disablePlayerChoices -= value;
		}
		
		public static event UnityAction OnEnablePlayerChoices
		{
			add => _enablePlayerChoices += value;
			remove => _enablePlayerChoices -= value;
		}
		
		public static event UnityAction OnSelectedChoiceAnimationDone
		{
			add => _selectedChoiceAnimationDone += value;
			remove => _selectedChoiceAnimationDone -= value;
		}
		
		public static event UnityAction OnShowBattle
		{
			add => _showBattle += value;
			remove => _showBattle -= value;
		}
		
		public static event UnityAction OnBattleBgCloseAnimationDone
		{
			add => _battleBgCloseAnimationDone += value;
			remove => _battleBgCloseAnimationDone -= value;
		}
		
		public static event UnityAction<RPSResultType> OnStartBattleAnimation
		{
			add => _startBattleAnimation += value;
			remove => _startBattleAnimation -= value;
		}

		public static event UnityAction OnBattleAnimationDone
		{
			add => _battleAnimationDone += value;
			remove => _battleAnimationDone -= value;
		}
		
		public static event UnityAction OnStartBattleBgOpenAnimation
		{
			add => _startBattleBgOpenAnimation += value;
			remove => _startBattleBgOpenAnimation -= value;
		}
		
		public static event UnityAction OnBattleBgOpenAnimationDone
		{
			add => _battleBgOpenAnimationDone += value;
			remove => _battleBgOpenAnimationDone -= value;
		}
		
		public static event UnityAction OnYouWonGame
		{
			add => _youWonGame += value;
			remove => _youWonGame -= value;
		}
		
		public static event UnityAction OnYouLostGame
		{
			add => _youLostGame += value;
			remove => _youLostGame -= value;
		}

		public static void RaisePlayChoiceSelectedEvent()
		{
			if (_playChoiceSelected == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaisePlayChoiceSelectedEvent)} raised, but nothing picked it up");
				return;
			}
			_playChoiceSelected.Invoke();
		}

		public static void RaiseDisablePlayerChoicesEvent()
		{
			if (_disablePlayerChoices == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseDisablePlayerChoicesEvent)} raised, but nothing picked it up");
				return;
			}
			_disablePlayerChoices.Invoke();
		}
		
		public static void RaiseEnablePlayerChoicesEvent()
		{
			if (_enablePlayerChoices == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseEnablePlayerChoicesEvent)} raised, but nothing picked it up");
				return;
			}
			_enablePlayerChoices.Invoke();
		}

		public static void RaiseSelectedChoiceAnimationDoneEvent()
		{
			if (_selectedChoiceAnimationDone == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseSelectedChoiceAnimationDoneEvent)} raised, but nothing picked it up");
				return;
			}
			_selectedChoiceAnimationDone.Invoke();
		}

		public static void RaiseShowBattleEvent()
		{
			if (_showBattle == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseShowBattleEvent)} raised, but nothing picked it up");
				return;
			}
			_showBattle.Invoke();
		}

		public static void RaiseBattleBgCloseAnimationDoneEvent()
		{
			if (_battleBgCloseAnimationDone == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseBattleBgCloseAnimationDoneEvent)} raised, but nothing picked it up");
				return;
			}
			_battleBgCloseAnimationDone.Invoke();
		}

		public static void RaiseStartBattleAnimationEvent(RPSResultType rpsResultType)
		{
			if (_startBattleAnimation == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseStartBattleAnimationEvent)} raised, but nothing picked it up");
				return;
			}
			_startBattleAnimation.Invoke(rpsResultType);
		}
		
		public static void RaiseBattleAnimationDoneEvent()
		{
			if (_battleAnimationDone == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseBattleAnimationDoneEvent)} raised, but nothing picked it up");
				return;
			}
			_battleAnimationDone.Invoke();
		}
		
		public static void RaiseStartBattleBgOpenAnimationEvent()
		{
			if (_startBattleBgOpenAnimation == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseStartBattleBgOpenAnimationEvent)} raised, but nothing picked it up");
				return;
			}
			_startBattleBgOpenAnimation.Invoke();
		}
		
		public static void RaiseBattleBgOpenAnimationDoneEvent()
		{
			if (_battleBgOpenAnimationDone == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseBattleBgOpenAnimationDoneEvent)} raised, but nothing picked it up");
				return;
			}
			_battleBgOpenAnimationDone.Invoke();
		}
		
		public static void RaiseYouWonGameEvent()
		{
			if (_youWonGame == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseYouWonGameEvent)} raised, but nothing picked it up");
				return;
			}
			_youWonGame.Invoke();
		}
		
		public static void RaiseYouLostGameEvent()
		{
			if (_youLostGame == null){
				LoggerService.LogWarning($"{nameof(RPSClientGameEvents)}::{nameof(RaiseYouLostGameEvent)} raised, but nothing picked it up");
				return;
			}
			_youLostGame.Invoke();
		}
	}
}