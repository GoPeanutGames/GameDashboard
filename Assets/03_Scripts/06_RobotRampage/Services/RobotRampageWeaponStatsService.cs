﻿using System.Collections.Generic;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageWeaponStatsService
	{
		private static readonly Dictionary<WeaponType, RobotRampageWeaponData> CurrentWeapons = new();
		private static readonly Dictionary<WeaponType, RobotRampageWeaponModifierData> CurrentWeaponModifiers = new();
		
		public static void Clear()
		{
			CurrentWeapons.Clear();
			CurrentWeaponModifiers.Clear();
		}

		public static void AddWeapon(RobotRampageWeaponData robotRampageWeaponData)
		{
			CurrentWeapons.Add(robotRampageWeaponData.WeaponType, robotRampageWeaponData);
			RobotRampageWeaponModifierData modifierData = new()
			{
				damageModifier = 1,
				penetrationModifier = 0,
				bulletAmountModifier = 0
			};
			CurrentWeaponModifiers.Add(robotRampageWeaponData.WeaponType, modifierData);
		}

		public static DamageType GetWeaponDamageType(WeaponType weaponType)
		{
			return CurrentWeapons[weaponType].DamageType;
		}
		
		public static float GetWeaponDamage(WeaponType weaponType, DamageType damageType)
		{
			return CurrentWeapons[weaponType].GetDamageForType(damageType) + CurrentWeapons[weaponType].GetDamageForType(damageType) * CurrentWeaponModifiers[weaponType].damageModifier;
		}

		public static int GetWeaponPenetration(WeaponType weaponType)
		{
			return CurrentWeapons[weaponType].Penetration + Mathf.FloorToInt(CurrentWeaponModifiers[weaponType].penetrationModifier);
		}

		public static int GetWeaponBulletAmount(WeaponType weaponType)
		{
			return CurrentWeapons[weaponType].BulletAmount + Mathf.FloorToInt(CurrentWeaponModifiers[weaponType].bulletAmountModifier);
		}

		public static void UpdateWeaponDamage(WeaponType weaponType, float modifier)
		{
			CurrentWeaponModifiers[weaponType].damageModifier += modifier;
		}

		public static void UpdateWeaponPenetration(WeaponType weaponType, float modifier)
		{
			CurrentWeaponModifiers[weaponType].penetrationModifier += modifier;
		}

		public static void UpdateWeaponBulletAmount(WeaponType weaponType, float modifier)
		{
			CurrentWeaponModifiers[weaponType].bulletAmountModifier += modifier;
		}
	}
}