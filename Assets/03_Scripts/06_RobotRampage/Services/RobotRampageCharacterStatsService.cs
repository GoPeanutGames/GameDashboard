using System.Collections.Generic;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageCharacterStatsService
	{
		private static RobotRampageCharacterData _currentCharacter;
		private static RobotRampageCharacterModifierData _currentModifiers;
		private static RobotRampageCharacterStats _calculatedStats;
		
		public static void SetCharacter(RobotRampageCharacterData characterData)
		{
			RobotRampageWeaponStatsService.Clear();
			_currentCharacter = characterData;
			InitModifiers();
			CalculateStats();
		}

		private static void InitModifiers()
		{
			_currentModifiers = new RobotRampageCharacterModifierData()
			{
				healthModifier = 1
			};			
		}
		
		private static void CalculateStats()
		{
			_calculatedStats = new RobotRampageCharacterStats()
			{
				attractionRange = _currentCharacter.AttractionRange,
				maxHealth = _currentCharacter.MaxHealth + _currentCharacter.MaxHealth * _currentModifiers.healthModifier
			};
		}

		public static float GetAttractionRange()
		{
			return _calculatedStats.attractionRange;
		}

		public static float GetMaxHp()
		{
			return _calculatedStats.maxHealth;
		}

		public static float GetExpToNextLevel(int levelIndex)
		{
			return _currentCharacter.LevelExp[levelIndex];
		}

		public static List<BaseUpgrade> GetStartingUpgrades()
		{
			return _currentCharacter.StartingWeapons;
		}
		
		public static void UpdateMaxHealth(float modifier)
		{
			_currentModifiers.healthModifier += modifier;
			CalculateStats();
			RobotRampageUpgradeEvents.RaiseRefreshStatsEvent();
		}
	}
}