using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(MobCollection) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageConfigs + nameof(MobCollection)
	)]
	public class MobCollection: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<MobMonsterData> _mobMonsters;

		public List<MobMonsterData> MobMonsters => _mobMonsters;

		public GameObject GetPrefabForMonster(MobType mobType)
		{
			return _mobMonsters.Find((m) => m.MobType == mobType).Prefab;
		}
	}
}