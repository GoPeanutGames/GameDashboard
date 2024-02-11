#if SERVER
using PeanutDashboard._02_BattleDash;
using PeanutDashboard.Shared.Logging;
using Unity.Netcode.Components;
#endif
using UnityEngine;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
using Unity.Netcode;

public class ServerBattleDashBullet : NetworkBehaviour, IFactionable
{

    [SerializeField]
    private FactionType _factionType;

    public FactionType FactionType => _factionType;
    
#if SERVER
    private NetworkAnimator _networkAnimator;
    private Vector3 _directionNormalised;

    private float _lifetime;
    private bool _initialised;
    private bool _destroyed;
    
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        _networkAnimator = GetComponent<NetworkAnimator>();
    }

    public void Initialise(Vector3 target)
    {
        LoggerService.LogInfo($"{nameof(ServerBattleDashBullet)}::{nameof(Initialise)} - target: {target}");
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
        this.transform.Translate(_directionNormalised * (BattleDashConfig.BulletSpeed * NetworkManager.ServerTime.FixedDeltaTime), Space.World);
        if (_lifetime < 0){
            this.GetComponent<NetworkObject>().Despawn();
            _destroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{nameof(ServerBattleDashBullet)}::{nameof(OnTriggerEnter2D)} - {other.name}");
        if (_destroyed){
            Debug.Log($"{nameof(ServerBattleDashBullet)}::{nameof(OnTriggerEnter2D)} - we're already destroyed, returning");
            return;
        }
        IFactionable otherFaction = other.GetComponent<IFactionable>();
        if (otherFaction == null || otherFaction.FactionType == this._factionType){
            Debug.Log($"{nameof(ServerBattleDashBullet)}::{nameof(OnTriggerEnter2D)} - other faction null or the same as ours, returning");
            return;
        }
        IDamageable otherDamageable = other.GetComponent<IDamageable>();
        if (otherDamageable == null){
            Debug.Log($"{nameof(ServerBattleDashBullet)}::{nameof(OnTriggerEnter2D)} - other object can't be damaged");
            return;
        }
        otherDamageable.TakeDamage(BattleDashConfig.BlummerBulletDamage);
        _destroyed = true;
        _networkAnimator.SetTrigger(Hit);
    }

#endif
    public void Delete()
    {
#if SERVER
        this.GetComponent<NetworkObject>().Despawn();
#endif
    }
}
