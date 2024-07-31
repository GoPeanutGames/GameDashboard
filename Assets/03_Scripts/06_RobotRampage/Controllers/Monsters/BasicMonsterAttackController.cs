using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class BasicMonsterAttackController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        protected float _timeToAttack;
        
        [SerializeField]
        protected float _cooldown;
        
        [SerializeField]
        protected float _damage;

        protected virtual void Update()
        {
            _timeToAttack -= Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player" && _timeToAttack < 0)
            {
                _timeToAttack = _cooldown;
                other.GetComponent<RobotRampagePlayerHealth>().DealDamage(_damage);
            }
        }
    }
}