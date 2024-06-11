using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(MobMonsterData) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(MobMonsterData)
	)]
	public class MobMonsterData: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private MobType _mobType;
		
		[SerializeField]
		private GameObject _prefab;

		public MobType MobType => _mobType;
		
		public GameObject Prefab => _prefab;
	}
}