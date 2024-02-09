using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Server
{
	public class ServerBattleDashPlayerShooting : NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Animator _animator;
		
		[SerializeField]
		private GameObject _bulletPrefab;
		
#if SERVER

		private Vector2 _bulletSpawnPoint;
		private Vector2 _currentTarget;
		private float _shootTimer = BattleDashConfig.ShootingCooldown;

		private void OnEnable()
		{
			ServerPlayerActionEvents.OnUpdatePlayerTarget += OnCurrentTargetUpdate;
			ServerPlayerActionEvents.OnUpdatePlayerBulletSpawnPoint += OnBulletSpawnPointUpdate;
		}

		private void OnCurrentTargetUpdate(Vector2 target)
		{
			_currentTarget = target;
		}
		
		private void OnBulletSpawnPointUpdate(Vector2 target)
		{
			_bulletSpawnPoint = target;
		}
		
		private void Update()
		{
			_shootTimer -= NetworkManager.ServerTime.FixedDeltaTime;
			if (_shootTimer <= 0){
				_shootTimer = BattleDashConfig.ShootingCooldown;
				_animator.SetTrigger("Shoot");
				GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint, Quaternion.identity);
				bullet.GetComponent<NetworkObject>().Spawn();
				bullet.GetComponent<ServerBattleDashBullet>().Initialise(_currentTarget);
			}
		}
		
		private void OnDisable()
		{
			ServerPlayerActionEvents.OnUpdatePlayerTarget -= OnCurrentTargetUpdate;
			ServerPlayerActionEvents.OnUpdatePlayerBulletSpawnPoint -= OnBulletSpawnPointUpdate;
		}
#endif
	}
}