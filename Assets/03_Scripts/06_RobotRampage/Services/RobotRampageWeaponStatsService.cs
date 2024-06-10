using System.Collections.Generic;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageWeaponStatsService
	{
		private static Dictionary<WeaponType, RobotRampageWeaponData> _currentWeapons = new();
		private static Dictionary<WeaponType, RobotRampageWeaponModifierData> _currentWeaponModifiers = new();

		public static void AddWeapon(RobotRampageWeaponData robotRampageWeaponData)
		{
			_currentWeapons.Add(robotRampageWeaponData.WeaponType, robotRampageWeaponData);
			RobotRampageWeaponModifierData modifierData = new()
			{
				damageModifier = 1,
				penetrationModifier = 0,
				bulletAmountModifier = 0
			};
			_currentWeaponModifiers.Add(robotRampageWeaponData.WeaponType, modifierData);
		}

		public static void Clear()
		{
			_currentWeapons.Clear();
			_currentWeaponModifiers.Clear();
		}
	}
}