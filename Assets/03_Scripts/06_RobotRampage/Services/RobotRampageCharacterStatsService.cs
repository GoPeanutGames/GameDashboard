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
				healthModifier = 0,
				speedModifier = 0,
				attractionRangeModifier = 0
			};			
		}
		
		private static void CalculateStats()
		{
			_calculatedStats = new RobotRampageCharacterStats()
			{
				attractionRange = _currentCharacter.AttractionRange + _currentCharacter.AttractionRange * _currentModifiers.attractionRangeModifier,
				maxHealth = _currentCharacter.MaxHealth + _currentCharacter.MaxHealth * _currentModifiers.healthModifier,
				speed = _currentCharacter.Speed + _currentCharacter.Speed * _currentModifiers.speedModifier
			};
		}

		public static float GetAttractionRange()
		{
			return _calculatedStats.attractionRange;
		}

		public static float GetSpeed()
		{
			return _calculatedStats.speed;
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

		public static void UpdateSpeed(float modifier)
		{
			_currentModifiers.speedModifier += modifier;
			CalculateStats();
			RobotRampageUpgradeEvents.RaiseRefreshStatsEvent();
		}

		public static void UpdateAttractionRange(float modifier)
		{
			_currentModifiers.attractionRangeModifier += modifier;
			CalculateStats();
			RobotRampageUpgradeEvents.RaiseRefreshStatsEvent();
		}
	}
}