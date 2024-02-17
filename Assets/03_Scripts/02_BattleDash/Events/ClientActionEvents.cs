using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ClientActionEvents
	{
		private static UnityAction<Vector2> _updatePlayerAim;
		private static UnityAction<Vector3> _updatePlayerVisualPosition;
		private static UnityAction<Vector2> _updatePlayerBulletSpawnPoint;
		private static UnityAction<Vector2> _mobilePlayerTouchShootPosition;
		private static UnityAction _playerRequestDisconnect;

		public static event UnityAction<Vector2> OnUpdatePlayerTarget
		{
			add => _updatePlayerAim += value;
			remove => _updatePlayerAim -= value;
		}
		
		public static event UnityAction<Vector2> OnUpdatePlayerBulletSpawnPoint
		{
			add => _updatePlayerBulletSpawnPoint += value;
			remove => _updatePlayerBulletSpawnPoint -= value;
		}
		
		public static event UnityAction<Vector3> OnUpdatePlayerVisualPosition
		{
			add => _updatePlayerVisualPosition += value;
			remove => _updatePlayerVisualPosition -= value;
		}
		
		public static event UnityAction<Vector2> OnMobilePlayerTouchShootPosition
		{
			add => _mobilePlayerTouchShootPosition += value;
			remove => _mobilePlayerTouchShootPosition -= value;
		}
		
		public static event UnityAction OnPlayerRequestDisconnect
		{
			add => _playerRequestDisconnect += value;
			remove => _playerRequestDisconnect -= value;
		}
		
		public static void RaiseUpdatePlayerAimEvent(Vector2 worldPosition)
		{
			if (_updatePlayerAim == null){
				LoggerService.LogWarning($"{nameof(ClientActionEvents)}::{nameof(RaiseUpdatePlayerAimEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerAim.Invoke(worldPosition);
		}
		
		public static void RaiseUpdatePlayerBulletSpawnPointEvent(Vector2 worldPosition)
		{
			if (_updatePlayerBulletSpawnPoint == null){
				LoggerService.LogWarning($"{nameof(ClientActionEvents)}::{nameof(RaiseUpdatePlayerBulletSpawnPointEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerBulletSpawnPoint.Invoke(worldPosition);
		}
		
		public static void RaiseUpdatePlayerVisualPositionEvent(Vector3 visualPosition)
		{
			if (_updatePlayerVisualPosition == null){
				LoggerService.LogWarning($"{nameof(ClientActionEvents)}::{nameof(RaiseUpdatePlayerVisualPositionEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerVisualPosition.Invoke(visualPosition);
		}
		
		public static void RaiseMobilePlayerTouchShootPositionEvent(Vector2 screenTouchPosition)
		{
			if (_mobilePlayerTouchShootPosition == null){
				LoggerService.LogWarning($"{nameof(ClientActionEvents)}::{nameof(RaiseMobilePlayerTouchShootPositionEvent)} raised, but nothing picked it up");
				return;
			}
			_mobilePlayerTouchShootPosition.Invoke(screenTouchPosition);
		}
		
		public static void RaisePlayerRequestDisconnectEvent()
		{
			if (_playerRequestDisconnect == null){
				LoggerService.LogWarning($"{nameof(ClientActionEvents)}::{nameof(RaisePlayerRequestDisconnectEvent)} raised, but nothing picked it up");
				return;
			}
			_playerRequestDisconnect.Invoke();
		}
	}
}