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
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private int _shotsPerSecond;

        [SerializeField]
        private float _timeToShoot;

        private void Start()
        {
            _timeToShoot = 1f / _shotsPerSecond;
        }

        private void Update()
        {
            _timeToShoot -= Time.deltaTime;
            if (_timeToShoot <= 0)
            {
                GameObject bullet = Instantiate(_bulletPrefab, this.transform.position + this.transform.right * 0.3f, Quaternion.identity);
                bullet.GetComponent<RobotRampageGunBullet>().SetStats(_weaponType, "Enemy", this.transform.right, 2.4f, 3f);
                _timeToShoot = 1f / _shotsPerSecond;
            }
        }
    }
}