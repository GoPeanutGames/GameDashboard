using System.Collections.Generic;
using PeanutDashboard._02_BattleDash.Data;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Config
{
	[CreateAssetMenu(
		fileName = nameof(AreaMonsterSpawnDefinition) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashConfigs + nameof(AreaMonsterSpawnDefinition)
	)]
	public class AreaMonsterSpawnDefinition: ScriptableObject
	{
		public List<AreaMonsterSpawn> areaMonsterSpawns;

		public void ResetArea()
		{
			Debug.Log($"{nameof(AreaMonsterSpawnDefinition)}::{nameof(ResetArea)} - {name}");
			foreach (AreaMonsterSpawn areaMonsterSpawn in areaMonsterSpawns){
				areaMonsterSpawn.spawned = false;
			}
		}
	}
}