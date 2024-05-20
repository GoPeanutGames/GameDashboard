using System;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageGunBullet: RobotRampageBaseBullet
    {
        [SerializeField]
        private Vector3 _direction;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _lifetime;

        public void SetStats(Vector3 direction, float speed, float lifetime)
        {
            _direction = direction;
            _speed = speed;
            _lifetime = lifetime;
        }
        
        private void Update()
        {
            _lifetime -= Time.deltaTime;
            this.transform.Translate(_direction * _speed * Time.deltaTime);
            if (_lifetime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}