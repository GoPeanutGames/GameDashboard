using PeanutDashboard._02_BattleDash;
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using UnityEngine;

public class BattleDashBullet : NetworkBehaviour
{
#if SERVER
    private Vector3 _directionNormalised;
    
    public void Initialise(Vector3 target)
    {
        LoggerService.LogInfo($"{nameof(BattleDashBullet)}::{nameof(Initialise)} - target: {target}");
        _directionNormalised = (target - this.transform.position).normalized;
        float angle = Mathf.Atan2(_directionNormalised.y, _directionNormalised.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        this.transform.Translate(_directionNormalised * (BattleDashConfig.BulletSpeed * NetworkManager.ServerTime.FixedDeltaTime));
    }
#endif
    
}
