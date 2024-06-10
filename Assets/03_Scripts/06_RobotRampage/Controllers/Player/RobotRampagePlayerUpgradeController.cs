using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerUpgradeController: MonoBehaviour
	{
		private void OnEnable()
		{
			RobotRampageUpgradeEvents.ApplyUpgrade += OnApplyUpgrade;
		}

		private void OnDisable()
		{
			RobotRampageUpgradeEvents.ApplyUpgrade -= OnApplyUpgrade;
		}

		private void OnApplyUpgrade(BaseUpgrade baseUpgrade)
		{
			switch (baseUpgrade.BaseUpgradeType){
				case BaseUpgradeType.Passive:
					ApplyPassive(baseUpgrade as PassiveUpgrade);
					break;
			}
		}

		private void ApplyPassive(PassiveUpgrade passiveUpgrade)
		{
			foreach (StatData upgradeLevelStatData in passiveUpgrade.UpgradeLevel.statDatas){
				UpdateCharacterStat(upgradeLevelStatData.statType, upgradeLevelStatData.statChange);
			}
		}

		private void ApplyAddWeapon(AddWeaponUpgrade addWeaponUpgrade)
		{
			
		}

		private void ApplyUpgradeWeapon(UpdateWeaponUpgrade updateWeaponUpgrade)
		{
			
		}

		private void UpdateCharacterStat(StatType statType, float modifier)
		{
			switch (statType){
				case StatType.Health:
					RobotRampageCharacterStatsService.UpdateMaxHealth(modifier);
					break;
			}
		}
	}
}