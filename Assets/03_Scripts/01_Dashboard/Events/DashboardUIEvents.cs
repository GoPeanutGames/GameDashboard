using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Dashboard.Events
{
	public class DashboardUIEvents : Singleton<DashboardUIEvents>
	{
		private UnityAction<bool> _showLogInUI;
		
		public event UnityAction<bool> ShowLogInUI
		{
			add => _showLogInUI += value;
			remove => _showLogInUI -= value;
		}
		
		public void RaiseShowLogInUIEvent(bool loggedIn)
		{
			if (_showLogInUI == null){
				LoggerService.LogWarning($"{nameof(DashboardUIEvents)}::{nameof(RaiseShowLogInUIEvent)} raised, but nothing picked it up");
				return;
			}
			_showLogInUI.Invoke(loggedIn);
		}
	}
}