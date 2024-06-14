using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage.Collection
{
	[CreateAssetMenu(
		fileName = nameof(UpgradeCollection) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageConfigs + nameof(UpgradeCollection)
	)]
	public class UpgradeCollection : ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<UpdatePassiveUpgrade> _updatePassiveUpgrades;

		[SerializeField]
		private List<AddPassiveUpgrade> _addPassiveUpgrades;
		
		[SerializeField]
		private List<AddWeaponUpgrade> _addWeaponUpgrades;

		[SerializeField]
		private List<UpdateWeaponUpgrade> _updateWeaponUpgrades;

		public List<BaseUpgrade> GetUpgrades()
		{
			List<BaseUpgrade> baseUpgrades = new List<BaseUpgrade>();
			baseUpgrades.AddRange(_updatePassiveUpgrades);
			baseUpgrades.AddRange(_addWeaponUpgrades);
			baseUpgrades.AddRange(_updateWeaponUpgrades);
			return baseUpgrades;
		}
	}
}