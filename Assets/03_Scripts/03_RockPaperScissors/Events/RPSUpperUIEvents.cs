using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSUpperUIEvents
	{
		private static UnityAction<string> _updateUpperSmallText;
		private static UnityAction<string> _updateUpperBigText;
		private static UnityAction<string> _updateEnemyNameText;
		private static UnityAction<string> _updateYourScoreText;
		private static UnityAction<string> _updateEnemyScoreText;
		private static UnityAction<Sprite> _updateYourChoiceImage;
		private static UnityAction<Sprite> _updateEnemyChoiceImage;
		private static UnityAction<Sprite> _updateUpperIndicator;
		private static UnityAction _hideYourScore;
		private static UnityAction _hideEnemyScore;
		private static UnityAction _showYourScore;
		private static UnityAction _showEnemyScore;
		private static UnityAction _hideIndicator;

		public static event UnityAction<string> OnUpdateUpperSmallText
		{
			add => _updateUpperSmallText += value;
			remove => _updateUpperSmallText -= value;
		}
		
		public static event UnityAction<string> OnUpdateUpperBigText
		{
			add => _updateUpperBigText += value;
			remove => _updateUpperBigText -= value;
		}
		
		public static event UnityAction<string> OnUpdateEnemyNameText
		{
			add => _updateEnemyNameText += value;
			remove => _updateEnemyNameText -= value;
		}
		
		public static event UnityAction<string> OnUpdateYourScoreText
		{
			add => _updateYourScoreText += value;
			remove => _updateYourScoreText -= value;
		}
		
		public static event UnityAction<string> OnUpdateEnemyScoreText
		{
			add => _updateEnemyScoreText += value;
			remove => _updateEnemyScoreText -= value;
		}
		
		public static event UnityAction<Sprite> OnUpdateYourChoiceImage
		{
			add => _updateYourChoiceImage += value;
			remove => _updateYourChoiceImage -= value;
		}
		
		public static event UnityAction<Sprite> OnUpdateEnemyChoiceImage
		{
			add => _updateEnemyChoiceImage += value;
			remove => _updateEnemyChoiceImage -= value;
		}
		
		public static event UnityAction<Sprite> OnUpdateUpperIndicator
		{
			add => _updateUpperIndicator += value;
			remove => _updateUpperIndicator -= value;
		}
		
		public static event UnityAction OnHideYourScore
		{
			add => _hideYourScore += value;
			remove => _hideYourScore -= value;
		}
		
		public static event UnityAction OnHideEnemyScore
		{
			add => _hideEnemyScore += value;
			remove => _hideEnemyScore -= value;
		}
		
		public static event UnityAction OnShowYourScore
		{
			add => _showYourScore += value;
			remove => _showYourScore -= value;
		}
		
		public static event UnityAction OnShowEnemyScore
		{
			add => _showEnemyScore += value;
			remove => _showEnemyScore -= value;
		}
		
		public static event UnityAction OnHideIndicator
		{
			add => _hideIndicator += value;
			remove => _hideIndicator -= value;
		}

		public static void RaiseUpdateUpperSmallTextEvent(string text)
		{
			if (_updateUpperSmallText == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateUpperSmallTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateUpperSmallText.Invoke(text);
		}

		public static void RaiseUpdateUpperBigTextEvent(string text)
		{
			if (_updateUpperBigText == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateUpperBigTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateUpperBigText.Invoke(text);
		}

		public static void RaiseUpdateEnemyNameTextEvent(string text)
		{
			if (_updateEnemyNameText == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateEnemyNameTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateEnemyNameText.Invoke(text);
		}
		
		public static void RaiseUpdateYourScoreTextEvent(string text)
		{
			if (_updateYourScoreText == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateYourScoreTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateYourScoreText.Invoke(text);
		}
		
		public static void RaiseUpdateEnemyScoreTextEvent(string text)
		{
			if (_updateEnemyScoreText == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateEnemyScoreTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateEnemyScoreText.Invoke(text);
		}
		
		public static void RaiseUpdateYourChoiceImageEvent(Sprite image)
		{
			if (_updateYourChoiceImage == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateYourChoiceImageEvent)} raised, but nothing picked it up");
				return;
			}
			_updateYourChoiceImage.Invoke(image);
		}
		
		public static void RaiseUpdateEnemyChoiceImageEvent(Sprite image)
		{
			if (_updateEnemyChoiceImage == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateEnemyChoiceImageEvent)} raised, but nothing picked it up");
				return;
			}
			_updateEnemyChoiceImage.Invoke(image);
		}
		
		public static void RaiseUpdateUpperIndicatorEvent(Sprite image)
		{
			if (_updateUpperIndicator == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseUpdateUpperIndicatorEvent)} raised, but nothing picked it up");
				return;
			}
			_updateUpperIndicator.Invoke(image);
		}
		
		public static void RaiseHideYourScoreEvent()
		{
			if (_hideYourScore == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseHideYourScoreEvent)} raised, but nothing picked it up");
				return;
			}
			_hideYourScore.Invoke();
		}
		
		public static void RaiseHideEnemyScoreEvent()
		{
			if (_hideEnemyScore == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseHideEnemyScoreEvent)} raised, but nothing picked it up");
				return;
			}
			_hideEnemyScore.Invoke();
		}
		
		public static void RaiseShowYourScoreEvent()
		{
			if (_showYourScore == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseShowYourScoreEvent)} raised, but nothing picked it up");
				return;
			}
			_showYourScore.Invoke();
		}
		
		public static void RaiseShowEnemyScoreEvent()
		{
			if (_showEnemyScore == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseShowEnemyScoreEvent)} raised, but nothing picked it up");
				return;
			}
			_showEnemyScore.Invoke();
		}
		
		public static void RaiseHideIndicatorEvent()
		{
			if (_hideIndicator == null){
				LoggerService.LogWarning($"{nameof(RPSUpperUIEvents)}::{nameof(RaiseHideIndicatorEvent)} raised, but nothing picked it up");
				return;
			}
			_hideIndicator.Invoke();
		}
	}
}