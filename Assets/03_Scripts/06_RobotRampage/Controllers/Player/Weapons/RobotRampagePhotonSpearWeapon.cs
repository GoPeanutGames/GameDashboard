using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampagePhotonSpearWeapon: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _bulletPrefab;
        
        [SerializeField]
        private WeaponType _weaponType;

        [SerializeField]
        private float _minAngle;

        [SerializeField]
        private float _maxAngle;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private int _shotsPerSecond;

        [SerializeField]
        private float _timeToShoot;

        [SerializeField]
        private DamageType _damageType;

        private void Start()
        {
            _damageType = RobotRampageWeaponStatsService.GetWeaponDamageType(_weaponType);
            _timeToShoot = 1f / _shotsPerSecond;
            _minAngle = -45;
            _maxAngle = 45;
        }

        private void Update()
        {
            _timeToShoot -= Time.deltaTime;
            if (_timeToShoot <= 0){
                int bulletAmount = RobotRampageWeaponStatsService.GetWeaponBulletAmount(_weaponType);
                for (int i = 0; i < bulletAmount; i++){
                    SpawnBullet(i+1, bulletAmount);
                }
                _timeToShoot = 1f / _shotsPerSecond;
            }
        }

        private void SpawnBullet(int bulletNumber, int bulletAmount)
        {
            float totalAngle = Mathf.Abs(_minAngle) + Mathf.Abs(_maxAngle);
            float angleAdd = totalAngle / (bulletAmount + 1);

            float bulletAngle = _minAngle + angleAdd * bulletNumber;
            Quaternion angleAxis = Quaternion.AngleAxis(bulletAngle, Vector3.forward);
            
            
            
            GameObject bullet = Instantiate(_bulletPrefab, this.transform.position + this.transform.right * 0.3f, Quaternion.identity);
            bullet.GetComponent<RobotRampageGunBullet>().SetStats(_weaponType, _damageType, "Enemy", angleAxis * this.transform.right, 2.4f, 3f);
        }
    }
}