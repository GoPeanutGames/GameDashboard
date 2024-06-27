using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageUIEvents
	{
		private static UnityAction<string> _showCentralNotification;

		private static UnityAction<float, float> _updatePlayerHealthBar;

		public static event UnityAction<string> OnShowCentralNotification
		{
			add => _showCentralNotification += value;
			remove => _showCentralNotification -= value;
		}
		
		public static event UnityAction<float, float> UpdatePlayerHealthBar
		{
			add => _updatePlayerHealthBar += value;
			remove => _updatePlayerHealthBar -= value;
		}
		
		public static void RaiseShowCentralNotificationEvent(string text)
		{
			if (_showCentralNotification == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUIEvents)}::{nameof(RaiseShowCentralNotificationEvent)} raised, but nothing picked it up");
				return;
			}
			_showCentralNotification.Invoke(text);
		}
		
		public static void RaiseUpdatePlayerHealthBarEvent(float currentHealth, float maxHealth)
		{
			if (_updatePlayerHealthBar == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUIEvents)}::{nameof(RaiseUpdatePlayerHealthBarEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerHealthBar.Invoke(currentHealth, maxHealth);
		}
	}
}