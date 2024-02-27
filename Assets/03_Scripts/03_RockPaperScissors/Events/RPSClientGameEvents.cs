using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSClientGameEvents
	{
		private static UnityAction _playChoiceSelected;
		private static UnityAction _disablePlayerChoices;
		private static UnityAction _selectedChoiceAnimationDone;
		private static UnityAction _showBattle;
        
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
	}
}