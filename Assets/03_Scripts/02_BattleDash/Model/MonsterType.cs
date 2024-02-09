using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Model
{
	[CreateAssetMenu(
		fileName = nameof(MonsterType) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashModels + nameof(MonsterType)
	)]
	public class MonsterType: ScriptableObject
	{
		public GameObject monsterPrefab;
		public float monsterSpeed;
		public int monsterHp;
		public int damageDealt;
	}
}