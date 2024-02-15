using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashClientUIEvents
	{
		private static UnityAction<bool> _showTooltips;
		private static UnityAction _hideTooltips;
		private static UnityAction _openEndGamePopup;
		private static UnityAction _closeEndGamePopup;
		private static UnityAction _showGameOver;
		private static UnityAction _showWon;

		public static event UnityAction<bool> OnShowTooltips
		{
			add => _showTooltips += value;
			remove => _showTooltips -= value;
		}
		
		public static event UnityAction OnHideTooltips
		{
			add => _hideTooltips += value;
			remove => _hideTooltips -= value;
		}
		
		public static event UnityAction OnOpenEndGamePopup
		{
			add => _openEndGamePopup += value;
			remove => _openEndGamePopup -= value;
		}
		
		public static event UnityAction OnCloseEndGamePopup
		{
			add => _closeEndGamePopup += value;
			remove => _closeEndGamePopup -= value;
		}
		
		public static event UnityAction OnShowGameOver
		{
			add => _showGameOver += value;
			remove => _showGameOver -= value;
		}
		
		public static event UnityAction OnShowWon
		{
			add => _showWon += value;
			remove => _showWon -= value;
		}
		
		public static void RaiseShowTooltipsEvent(bool first)
		{
			if (_showTooltips == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseShowTooltipsEvent)} raised, but nothing picked it up");
				return;
			}
			_showTooltips.Invoke(first);
		}
		
		public static void RaiseHideTooltipsEvent()
		{
			if (_hideTooltips == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseHideTooltipsEvent)} raised, but nothing picked it up");
				return;
			}
			_hideTooltips.Invoke();
		}
		
		public static void RaiseOpenEndGamePopupEvent()
		{
			if (_openEndGamePopup == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseOpenEndGamePopupEvent)} raised, but nothing picked it up");
				return;
			}
			_openEndGamePopup.Invoke();
		}
		
		public static void RaiseCloseEndGamePopupEvent()
		{
			if (_closeEndGamePopup == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseCloseEndGamePopupEvent)} raised, but nothing picked it up");
				return;
			}
			_closeEndGamePopup.Invoke();
		}
		
		public static void RaiseShowGameOverEvent()
		{
			if (_showGameOver == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseShowGameOverEvent)} raised, but nothing picked it up");
				return;
			}
			_showGameOver.Invoke();
		}
		
		public static void RaiseShowWonEvent()
		{
			if (_showWon == null){
				LoggerService.LogWarning($"{nameof(BattleDashClientUIEvents)}::{nameof(RaiseShowWonEvent)} raised, but nothing picked it up");
				return;
			}
			_showWon.Invoke();
		}
	}
}