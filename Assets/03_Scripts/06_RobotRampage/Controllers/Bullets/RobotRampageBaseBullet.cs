using System;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBaseBullet: MonoBehaviour
    {
        [SerializeField]
        private string _tagToDamage;

        [SerializeField]
        private float _damageToDeal;

        public virtual void Setup(string tagToDamage, float damage)
        {
            _tagToDamage = tagToDamage;
            _damageToDeal = damage;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals(_tagToDamage))
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}