using PeanutDashboard.Shared.Logging;
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
	}
}