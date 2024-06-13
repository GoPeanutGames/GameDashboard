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
		private DamageType _damageType;

		[SerializeField]
		private GameObject _prefab;

		[SerializeField]
		private float _damageForType;

		[SerializeField]
		private int _penetration;

		[SerializeField]
		private int _bulletAmount;

		public DamageType DamageType => _damageType;
		
		public WeaponType WeaponType => _weaponType;

		public GameObject Prefab => _prefab;

		public int Penetration => _penetration;

		public int BulletAmount => _bulletAmount;

		public float GetDamageForType(DamageType damageType)
		{
			return _damageType == damageType ? _damageForType : 0;
		}
	}
}