using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player
{
	public class BattleDashPlayerShooting : NetworkBehaviour
	{
		private readonly NetworkVariable<Vector2> _currentTarget = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Animator _animator;
		
#if SERVER
		private const float ShootingCooldown = 0.5f;
		private float _shootTimer = 0.5f;
#endif

		private void OnEnable()
		{
			ClientActionEvents.OnUpdatePlayerAim += ClientUpdateTarget;
		}

		private void ClientUpdateTarget(Vector2 target)
		{
			_currentTarget.Value = target;
		}

#if SERVER
		private void Update()
		{
			_shootTimer -= NetworkManager.ServerTime.FixedDeltaTime;
			if (_shootTimer <= 0){
				_shootTimer = ShootingCooldown;
				_animator.SetTrigger("Shoot");
			}
		}
#endif

		private void OnDisable()
		{
			ClientActionEvents.OnUpdatePlayerAim -= ClientUpdateTarget;
		}
	}
}