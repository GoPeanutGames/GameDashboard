using PeanutDashboard._02_BattleDash;
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using UnityEngine;

public class BattleDashBullet : NetworkBehaviour
{
#if SERVER
    private Vector3 _directionNormalised;

    private float _lifetime;
    private bool _initialised;
    private bool _destroyed;
    
    public void Initialise(Vector3 target)
    {
        LoggerService.LogInfo($"{nameof(BattleDashBullet)}::{nameof(Initialise)} - target: {target}");
        _directionNormalised = (target - this.transform.position).normalized;
        float angle = Mathf.Atan2(_directionNormalised.y, _directionNormalised.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _lifetime = BattleDashConfig.BulletLifetime;
        _initialised = true;
    }

    private void Update()
    {
        if (!_initialised || _destroyed){
            return;
        }
        _lifetime -= NetworkManager.ServerTime.FixedDeltaTime;
        this.transform.Translate(_directionNormalised * (BattleDashConfig.BulletSpeed * NetworkManager.ServerTime.FixedDeltaTime));
        if (_lifetime < 0){
            this.GetComponent<NetworkObject>().Despawn();
            _destroyed = true;
        }
    }
#endif
    
}
