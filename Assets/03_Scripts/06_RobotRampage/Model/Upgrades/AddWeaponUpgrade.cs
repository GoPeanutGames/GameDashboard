using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(AddWeaponUpgrade) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(AddWeaponUpgrade)
	)]
	public class AddWeaponUpgrade: BaseUpgrade
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private WeaponType _weaponType;

		[SerializeField]
		private BaseUpgrade _nextUpgrade;

		public WeaponType WeaponType => _weaponType;

		public BaseUpgrade NextUpgrade => _nextUpgrade;
	}
}