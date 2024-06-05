using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageUpgradeEvents
	{
		private static UnityAction _triggerUpgradesUI;
		private static UnityAction _onUpgradeChosen;

		public static event UnityAction OnTriggerUpgradesUI
		{
			add => _triggerUpgradesUI += value;
			remove => _triggerUpgradesUI -= value;
		}
		
		public static event UnityAction OnUpgradeChosen
		{
			add => _onUpgradeChosen += value;
			remove => _onUpgradeChosen -= value;
		}
		
		public static void RaiseTriggerUpgradesUIEvent()
		{
			if (_triggerUpgradesUI == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUpgradeEvents)}::{nameof(RaiseTriggerUpgradesUIEvent)} raised, but nothing picked it up");
				return;
			}
			_triggerUpgradesUI.Invoke();
		}
		
		public static void RaiseOnUpgradeChosenEvent()
		{
			if (_onUpgradeChosen == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUpgradeEvents)}::{nameof(RaiseOnUpgradeChosenEvent)} raised, but nothing picked it up");
				return;
			}
			_onUpgradeChosen.Invoke();
		}
	}
}