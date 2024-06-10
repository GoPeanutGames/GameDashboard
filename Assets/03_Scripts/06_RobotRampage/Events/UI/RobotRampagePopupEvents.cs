using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampagePopupEvents
	{
		private static UnityAction _openVictoryPopup;
		private static UnityAction _openDefeatPopup;

		public static event UnityAction OnOpenVictoryPopup
		{
			add => _openVictoryPopup += value;
			remove => _openVictoryPopup -= value;
		}
		
		public static event UnityAction OnOpenDefeatPopup
		{
			add => _openDefeatPopup += value;
			remove => _openDefeatPopup -= value;
		}
		
		public static void RaiseOpenVictoryPopupEvent()
		{
			if (_openVictoryPopup == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePopupEvents)}::{nameof(RaiseOpenVictoryPopupEvent)} raised, but nothing picked it up");
				return;
			}
			_openVictoryPopup.Invoke();
		}
		
		public static void RaiseOpenDefeatPopupEvent()
		{
			if (_openDefeatPopup == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePopupEvents)}::{nameof(RaiseOpenDefeatPopupEvent)} raised, but nothing picked it up");
				return;
			}
			_openDefeatPopup.Invoke();
		}
	}
}