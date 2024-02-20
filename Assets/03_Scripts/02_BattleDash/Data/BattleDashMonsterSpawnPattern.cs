using System;
using PeanutDashboard._02_BattleDash.Config;
using PeanutDashboard._02_BattleDash.Model;

namespace PeanutDashboard._02_BattleDash.Data
{
	[Serializable]
	public class BattleDashMonsterSpawnPattern
	{
		public BattleDashMonsterType battleDashMonsterType;
		public BattleDashMonsterSpawnLocation battleDashMonsterSpawnLocation;
		public int amount;
		public float timeBetweenSpawns;
		public float timeElapsed;
		public float timeToBeginSpawn;
	}

	public static class MonsterSpawnPatternExtension
	{
		public static BattleDashMonsterSpawnPattern MakeCopy(this BattleDashMonsterSpawnPattern battleDashMonsterSpawnPattern)
		{
			return new BattleDashMonsterSpawnPattern()
			{
				amount = battleDashMonsterSpawnPattern.amount,
				battleDashMonsterSpawnLocation = battleDashMonsterSpawnPattern.battleDashMonsterSpawnLocation,
				battleDashMonsterType = battleDashMonsterSpawnPattern.battleDashMonsterType,
				timeBetweenSpawns = battleDashMonsterSpawnPattern.timeBetweenSpawns,
				timeElapsed = battleDashMonsterSpawnPattern.timeElapsed,
				timeToBeginSpawn = battleDashMonsterSpawnPattern.timeToBeginSpawn
			};
		}
	}
}