using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ClientUIEvents
	{
		private static UnityAction<bool> _showTooltips;
		private static UnityAction _hideTooltips;

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
		
		public static void RaiseShowTooltipsEvent(bool first)
		{
			if (_showTooltips == null){
				LoggerService.LogWarning($"{nameof(ClientUIEvents)}::{nameof(RaiseShowTooltipsEvent)} raised, but nothing picked it up");
				return;
			}
			_showTooltips.Invoke(first);
		}
		
		public static void RaiseHideTooltipsEvent()
		{
			if (_hideTooltips == null){
				LoggerService.LogWarning($"{nameof(ClientUIEvents)}::{nameof(RaiseHideTooltipsEvent)} raised, but nothing picked it up");
				return;
			}
			_hideTooltips.Invoke();
		}
	}
}