using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageUIEvents
	{
		private static UnityAction<string> _showCentralNotification;

		public static event UnityAction<string> OnShowCentralNotification
		{
			add => _showCentralNotification += value;
			remove => _showCentralNotification -= value;
		}
		
		
		public static void RaiseShowCentralNotificationEvent(string text)
		{
			if (_showCentralNotification == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUIEvents)}::{nameof(RaiseShowCentralNotificationEvent)} raised, but nothing picked it up");
				return;
			}
			_showCentralNotification.Invoke(text);
		}
	}
}