using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageUpgradeEvents
	{
		private static UnityAction _triggerUpgradesUI;
		private static UnityAction _onUpgradeChosen;
		private static UnityAction<BaseUpgrade> _applyUpgrade;
		private static UnityAction _refreshStats;

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
		
		public static event UnityAction<BaseUpgrade> ApplyUpgrade
		{
			add => _applyUpgrade += value;
			remove => _applyUpgrade -= value;
		}
		
		public static event UnityAction OnRefreshStats
		{
			add => _refreshStats += value;
			remove => _refreshStats -= value;
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
		
		public static void RaiseApplyUpgradeEvent(BaseUpgrade baseUpgrade)
		{
			if (_applyUpgrade == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUpgradeEvents)}::{nameof(RaiseApplyUpgradeEvent)} raised, but nothing picked it up");
				return;
			}
			_applyUpgrade.Invoke(baseUpgrade);
		}
		
		public static void RaiseRefreshStatsEvent()
		{
			if (_refreshStats == null){
				LoggerService.LogWarning($"{nameof(RobotRampageUpgradeEvents)}::{nameof(RaiseRefreshStatsEvent)} raised, but nothing picked it up");
				return;
			}
			_refreshStats.Invoke();
		}
	}
}