using System.Collections.Generic;
using PeanutDashboard._02_BattleDash.Data;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Config
{
	[CreateAssetMenu(
		fileName = nameof(BattleDashAreaMonsterSpawnDefinition) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashConfigs + nameof(BattleDashAreaMonsterSpawnDefinition)
	)]
	public class BattleDashAreaMonsterSpawnDefinition: ScriptableObject
	{
		public List<BattleDashAreaMonsterSpawn> areaMonsterSpawns;

		public void ResetArea()
		{
			Debug.Log($"{nameof(BattleDashAreaMonsterSpawnDefinition)}::{nameof(ResetArea)} - {name}");
			foreach (BattleDashAreaMonsterSpawn areaMonsterSpawn in areaMonsterSpawns){
				areaMonsterSpawn.spawned = false;
			}
		}
	}
}