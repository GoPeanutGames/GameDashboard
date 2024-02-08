using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Config
{
	[CreateAssetMenu(
		fileName = nameof(MonsterSpawnLocation) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashConfigs + nameof(MonsterSpawnLocation)
	)]
	public class MonsterSpawnLocation: ScriptableObject
	{
		public Vector3 location;
	}
}