﻿using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageGunBullet: RobotRampageBaseBullet
    {
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Vector3 _direction;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _lifetime;

        public void SetStats(WeaponType weaponType, DamageType damageType, string tagToDamage, Vector3 direction, float speed, float lifetime)
        {
            Setup(weaponType, tagToDamage, damageType);
            _direction = direction;
            _speed = speed;
            _lifetime = lifetime;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
        private void Update()
        {
            _lifetime -= Time.deltaTime;
            this.transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
            if (_lifetime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}