using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Config
{
	[CreateAssetMenu(
		fileName = nameof(BattleDashMonsterSpawnLocation) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashConfigs + nameof(BattleDashMonsterSpawnLocation)
	)]
	public class BattleDashMonsterSpawnLocation: ScriptableObject
	{
		public Vector3 location;
	}
}