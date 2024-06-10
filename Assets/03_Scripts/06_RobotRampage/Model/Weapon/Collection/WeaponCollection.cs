using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage.Collection
{
	[CreateAssetMenu(
		fileName = nameof(WeaponCollection) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageConfigs + nameof(WeaponCollection)
	)]
	public class WeaponCollection: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<RobotRampageWeaponData> _weaponDatas;

		public RobotRampageWeaponData GetWeaponData(WeaponType weaponType)
		{
			return _weaponDatas.Find((w) => w.WeaponType == weaponType);
		}
	}
}