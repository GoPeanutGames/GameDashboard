namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageCharacterStatsService
	{
		private static RobotRampageCharacterData _currentCharacter;
		private static RobotRampageCharacterStats _calculatedStats;
		
		public static void SetCharacter(RobotRampageCharacterData characterData)
		{
			_currentCharacter = characterData;
			CalculateStats();
		}

		private static void CalculateStats()
		{
			_calculatedStats = new RobotRampageCharacterStats()
			{
				attractionRange = _currentCharacter.AttractionRange
			};
		}

		public static float GetAttractionRange()
		{
			return _calculatedStats.attractionRange;
		}
	}
}