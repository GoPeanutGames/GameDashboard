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

        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private bool _ignoreTriggers;

        public virtual void Setup(string tagToDamage, float damage)
        {
            _tagToDamage = tagToDamage;
            _damageToDeal = damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals(_tagToDamage) && !_ignoreTriggers)
            {
                _ignoreTriggers = true;
                other.GetComponent<RobotRampageMonsterController>().Damage(_damageToDeal);
                Destroy(this.gameObject);
            }
        }
    }
}