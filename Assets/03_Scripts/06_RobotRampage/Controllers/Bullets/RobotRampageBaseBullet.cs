using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBaseBullet: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private string _tagToDamage;

        [SerializeField]
        private float _damageToDeal;
        
        [SerializeField]
        private int _penetration;

        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private bool _ignoreTriggers;

        protected void Setup(WeaponType weaponType, string tagToDamage)
        {
            _tagToDamage = tagToDamage;
            _damageToDeal = RobotRampageWeaponStatsService.GetWeaponDamage(weaponType);
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