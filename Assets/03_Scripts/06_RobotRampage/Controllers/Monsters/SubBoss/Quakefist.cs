using UnityEngine;

namespace PeanutDashboard._06_RobotRampage.SubBoss
{
    public class Quakefist: RobotRampageMonsterController
    {
        [SerializeField]
        private float _cooldown;
        
        [SerializeField]
        private float _damage;
        
        [SerializeField]
        private QuakefistAttackTrigger _quakefistAttackTrigger;
        
        protected override void Start()
        {
            base.Start();
            _quakefistAttackTrigger.Initialise(_cooldown, _damage);
        }
    }
}