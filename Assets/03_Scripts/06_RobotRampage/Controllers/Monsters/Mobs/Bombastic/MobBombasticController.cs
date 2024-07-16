using System.Collections.Generic;
using System.Linq;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class MobBombasticController: RobotRampageMonsterController
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private LayerMask _layersToDamage;

        [SerializeField]
        private float _explosionRadius;

        [SerializeField]
        private float _distanceToExplode;

        [SerializeField]
        private bool _drawGizmos;
        
        protected override void Update()
        {
            base.Update();
            if (Vector3.Distance(this.transform.position, RobotRampagePlayerController.currentPosition) < _distanceToExplode)
            {
                Explosion();
                Destroy(this.gameObject);
            }
        }

        protected override void OnKilled()
        {
            Explosion();
            base.OnKilled();
        }

        private void Explosion()
        {
            EffectEvents.RaiseSpawnEffectAt(EffectType.BombasticExplosion, this.transform.position);
            List<Collider2D> allToDamage =
                Physics2D.OverlapCircleAll(this.transform.position, _explosionRadius, _layersToDamage).ToList();
            foreach (Collider2D colliderToDamage in allToDamage)
            {
                float distance = Vector3.Distance(colliderToDamage.transform.position, this.transform.position);
                if (_damage * (1 - distance / _explosionRadius) < 0)
                {
                    continue;
                }
                RobotRampagePlayerHealth playerHealth = colliderToDamage.GetComponent<RobotRampagePlayerHealth>();
                RobotRampageMonsterController monsterController = colliderToDamage.GetComponent<RobotRampageMonsterController>();
                if (playerHealth)
                {
                    playerHealth.DealDamage(_damage * (1 - distance/_explosionRadius));
                }
                if (monsterController != null && monsterController != this)
                {
                    monsterController.Damage(_damage * (1 - distance/_explosionRadius));
                }
            }
            allToDamage.Clear();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _distanceToExplode);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, _explosionRadius);
        }
#endif
    }
}