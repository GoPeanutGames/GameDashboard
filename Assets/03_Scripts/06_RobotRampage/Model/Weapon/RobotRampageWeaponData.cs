using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(RobotRampageWeaponData) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(RobotRampageWeaponData)
	)]
	public class RobotRampageWeaponData: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private WeaponType _weaponType;

		[SerializeField]
		private GameObject _prefab;

		[SerializeField]
		private float _damage;

		[SerializeField]
		private int _penetration;

		[SerializeField]
		private int _bulletAmount;

		public WeaponType WeaponType => _weaponType;

		public GameObject Prefab => _prefab;

		public float Damage => _damage;

		public int Penetration => _penetration;

		public int BulletAmount => _bulletAmount;
	}
}