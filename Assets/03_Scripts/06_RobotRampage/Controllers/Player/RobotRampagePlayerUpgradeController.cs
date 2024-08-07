using PeanutDashboard._06_RobotRampage.Collection;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerUpgradeController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampagePlayerController _playerController;
		
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
				case BaseUpgradeType.AddPassive:
					ApplyAddPassive(baseUpgrade as AddPassiveUpgrade);
					break;
				case BaseUpgradeType.UpdatePassive:
					ApplyUpgradePassive(baseUpgrade as UpdatePassiveUpgrade);
					break;
				case BaseUpgradeType.AddWeapon:
					ApplyAddWeapon(baseUpgrade as AddWeaponUpgrade);
					break;
				case BaseUpgradeType.UpdateWeapon:
					ApplyUpgradeWeapon(baseUpgrade as UpdateWeaponUpgrade);
					break;
			}
		}

		private void ApplyAddPassive(AddPassiveUpgrade addPassiveUpgrade)
		{
			foreach (StatData upgradeLevelStatData in addPassiveUpgrade.UpgradeLevel.statDatas){
				UpdateCharacterStat(upgradeLevelStatData.statType, upgradeLevelStatData.statChange);
			}
		}

		private void ApplyUpgradePassive(UpdatePassiveUpgrade updatePassiveUpgrade)
		{
			foreach (StatData upgradeLevelStatData in updatePassiveUpgrade.UpgradeLevel.statDatas){
				UpdateCharacterStat(upgradeLevelStatData.statType, upgradeLevelStatData.statChange);
			}
		}

		private void ApplyAddWeapon(AddWeaponUpgrade addWeaponUpgrade)
		{
			RobotRampageWeaponData weaponData = _weaponCollection.GetWeaponData(addWeaponUpgrade.WeaponType);
			RobotRampageWeaponStatsService.AddWeapon(weaponData);
			Instantiate(weaponData.Prefab, _playerController.GetWeaponParent().transform);
		}

		private void ApplyUpgradeWeapon(UpdateWeaponUpgrade updateWeaponUpgrade)
		{
			foreach (StatData upgradeLevelStatData in updateWeaponUpgrade.UpgradeLevel.statDatas){
				UpdateWeaponStat(updateWeaponUpgrade.WeaponType, upgradeLevelStatData.statType, upgradeLevelStatData.statChange);
			}
		}

		private void UpdateCharacterStat(StatType statType, float modifier)
		{
			switch (statType){
				case StatType.Health:
					RobotRampageCharacterStatsService.UpdateMaxHealth(modifier);
					break;
				case StatType.Movement:
					RobotRampageCharacterStatsService.UpdateSpeed(modifier);
					break;
				case StatType.AttractionRange:
					RobotRampageCharacterStatsService.UpdateAttractionRange(modifier);
					break;
			}
		}

		private void UpdateWeaponStat(WeaponType weaponType, StatType statType, float modifier)
		{
			switch (statType){
				case StatType.PlasmaStrength:
					RobotRampageWeaponStatsService.UpdateWeaponDamage(weaponType, modifier);
					break;
				case StatType.Penetration:
					RobotRampageWeaponStatsService.UpdateWeaponPenetration(weaponType, modifier);
					break;
				case StatType.BulletAmount:
					RobotRampageWeaponStatsService.UpdateWeaponBulletAmount(weaponType, modifier);
					break;
				case StatType.AOE:
					RobotRampageWeaponStatsService.UpdateWeaponAOE(weaponType, modifier);
					break;
				case StatType.Cooldown:
					RobotRampageWeaponStatsService.UpdateCooldown(weaponType, modifier);
					break;
			}
		}
	}
}