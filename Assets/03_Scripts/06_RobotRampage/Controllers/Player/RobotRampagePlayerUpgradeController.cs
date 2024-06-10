using PeanutDashboard._06_RobotRampage.Collection;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerUpgradeController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _weaponParent;
		
		[SerializeField]
		private WeaponCollection _weaponCollection;
		
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
				case BaseUpgradeType.AddWeapon:
					ApplyAddWeapon(baseUpgrade as AddWeaponUpgrade);
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
			RobotRampageWeaponData weaponData = _weaponCollection.GetWeaponData(addWeaponUpgrade.WeaponType);
			RobotRampageWeaponStatsService.AddWeapon(weaponData);
			Instantiate(weaponData.Prefab, _weaponParent.transform);
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