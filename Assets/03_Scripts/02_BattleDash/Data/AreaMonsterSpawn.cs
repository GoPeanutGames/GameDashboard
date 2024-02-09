using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Data
{
	[Serializable]
	public class AreaMonsterSpawn
	{
		public float areaPercentage;
		public bool spawned;
		
		[SerializeField]
		private List<MonsterSpawnPattern> _monsterSpawnPatterns;

		public List<MonsterSpawnPattern> GetPatterns()
		{
			return _monsterSpawnPatterns.Select(monsterSpawnPattern => monsterSpawnPattern.MakeCopy()).ToList();
		}
	}
}