using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Model
{
	[CreateAssetMenu(
		fileName = nameof(BattleDashMonsterType) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashModels + nameof(BattleDashMonsterType)
	)]
	public class BattleDashMonsterType: ScriptableObject
	{
		public GameObject monsterPrefab;
		public float monsterSpeed;
		public int monsterHp;
		public int damageDealt;
	}
}