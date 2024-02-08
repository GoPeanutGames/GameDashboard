using System;
using PeanutDashboard._02_BattleDash.Config;
using PeanutDashboard._02_BattleDash.Model;

namespace PeanutDashboard._02_BattleDash.Data
{
	[Serializable]
	public class MonsterSpawnPattern
	{
		public MonsterType monsterType;
		public MonsterSpawnLocation monsterSpawnLocation;
		public int amount;
		public float timeBetweenSpawns;
		public float timeElapsed;
		public float timeToBeginSpawn;
	}

	public static class MonsterSpawnPatternExtension
	{
		public static MonsterSpawnPattern MakeCopy(this MonsterSpawnPattern monsterSpawnPattern)
		{
			return new MonsterSpawnPattern()
			{
				amount = monsterSpawnPattern.amount,
				monsterSpawnLocation = monsterSpawnPattern.monsterSpawnLocation,
				monsterType = monsterSpawnPattern.monsterType,
				timeBetweenSpawns = monsterSpawnPattern.timeBetweenSpawns,
				timeElapsed = monsterSpawnPattern.timeElapsed,
				timeToBeginSpawn = monsterSpawnPattern.timeToBeginSpawn
			};
		}
	}
}