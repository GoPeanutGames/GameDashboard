using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Data
{
	[Serializable]
	public class BattleDashAreaMonsterSpawn
	{
		public float areaPercentage;
		public bool spawned;
		
		[SerializeField]
		private List<BattleDashMonsterSpawnPattern> _monsterSpawnPatterns;

		public List<BattleDashMonsterSpawnPattern> GetPatterns()
		{
			return _monsterSpawnPatterns.Select(monsterSpawnPattern => monsterSpawnPattern.MakeCopy()).ToList();
		}
	}
}