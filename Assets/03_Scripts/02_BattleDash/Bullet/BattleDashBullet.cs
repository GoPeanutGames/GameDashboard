using Unity.Netcode;
using UnityEngine;

public class BattleDashBullet : NetworkBehaviour
{
    private const float Speed = 10;
    private Vector3 _direction;
    
    public void Initialise(Vector3 target)
    {
        _direction = target - this.transform.position;
    }

    private void Update()
    {
        this.transform.Translate(_direction * (Speed * NetworkManager.ServerTime.FixedDeltaTime));
    }
}
