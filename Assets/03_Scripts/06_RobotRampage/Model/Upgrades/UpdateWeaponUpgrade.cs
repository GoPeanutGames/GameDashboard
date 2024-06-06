﻿using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(UpdateWeaponUpgrade) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(UpdateWeaponUpgrade)
	)]
	public class UpdateWeaponUpgrade: BaseUpgrade
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private WeaponType _weaponType;

		[SerializeField]
		private UpgradeLevel _upgradeLevel;

		public WeaponType WeaponType => _weaponType;

		public UpgradeLevel UpgradeLevel => _upgradeLevel;
	}
}