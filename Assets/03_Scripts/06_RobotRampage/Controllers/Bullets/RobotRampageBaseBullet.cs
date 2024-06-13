using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBaseBullet: MonoBehaviour
    {
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private string _tagToDamage;
        
        [SerializeField]
        private bool _ignoreTriggers;

        [SerializeField]
        private float _damageToDeal;

        [SerializeField]
        private int _penetration;
        
        [SerializeField]
        private DamageType _damageType;

        protected void Setup(WeaponType weaponType, string tagToDamage, DamageType damageType)
        {
            _tagToDamage = tagToDamage;
            _damageType = damageType;
            _damageToDeal = RobotRampageWeaponStatsService.GetWeaponDamage(weaponType, damageType);
            _penetration = RobotRampageWeaponStatsService.GetWeaponPenetration(weaponType);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals(_tagToDamage) && !_ignoreTriggers)
            {
                other.GetComponent<RobotRampageMonsterController>().Damage(_damageToDeal);
                _penetration--;
                if (_penetration < 0){
                    _ignoreTriggers = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}