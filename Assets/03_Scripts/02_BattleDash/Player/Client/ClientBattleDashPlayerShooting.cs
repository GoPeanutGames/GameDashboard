using PeanutDashboard._02_BattleDash.Events;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class ClientBattleDashPlayerShooting : NetworkBehaviour
	{
		private readonly NetworkVariable<Vector2> _currentTarget = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
		private readonly NetworkVariable<Vector2> _spawnPoint = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

		private void OnEnable()
		{
#if SERVER
			_currentTarget.OnValueChanged += OnCurrentTargetValueChangedOnServer;
			_spawnPoint.OnValueChanged += OnBulletSpawnPointValueChangedOnServer;
#else
			ClientActionEvents.OnUpdatePlayerTarget += ClientUpdateTarget;
			ClientActionEvents.OnUpdatePlayerBulletSpawnPoint += ClientUpdateBulletSpawnPoint;
#endif
		}


#if SERVER
		private void OnCurrentTargetValueChangedOnServer(Vector2 _, Vector2 newValue)
		{
			ServerPlayerActionEvents.RaiseUpdatePlayerAimEvent(newValue);
		}

		private void OnBulletSpawnPointValueChangedOnServer(Vector2 _, Vector2 newValue)
		{
			ServerPlayerActionEvents.RaiseUpdatePlayerBulletSpawnPointEvent(newValue);
		}
#else
		private void ClientUpdateTarget(Vector2 target)
		{
			_currentTarget.Value = target;
		}

		private void ClientUpdateBulletSpawnPoint(Vector2 spawnPoint)
		{
			_spawnPoint.Value = spawnPoint;
		}
#endif


		private void OnDisable()
		{
#if SERVER
			_currentTarget.OnValueChanged -= OnCurrentTargetValueChangedOnServer;
			_spawnPoint.OnValueChanged -= OnBulletSpawnPointValueChangedOnServer;
#else
			ClientActionEvents.OnUpdatePlayerTarget -= ClientUpdateTarget;
			ClientActionEvents.OnUpdatePlayerBulletSpawnPoint -= ClientUpdateBulletSpawnPoint;
#endif
		}
	}
}