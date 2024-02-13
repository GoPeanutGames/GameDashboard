using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ServerPlayerInputEvents
	{
		private static UnityAction<KeyCode> _playerInputKeyDown;
		private static UnityAction<KeyCode> _playerInputKeyUp;
		private static UnityAction<Vector2> _playerMobileTouchPosition;
		
		public static event UnityAction<KeyCode> PlayerInputKeyDown
		{
			add => _playerInputKeyDown += value;
			remove => _playerInputKeyDown -= value;
		}
		
		public static event UnityAction<KeyCode> PlayerInputKeyUp
		{
			add => _playerInputKeyUp += value;
			remove => _playerInputKeyUp -= value;
		}
		
		public static event UnityAction<Vector2> PlayerMobileTouchPosition
		{
			add => _playerMobileTouchPosition += value;
			remove => _playerMobileTouchPosition -= value;
		}
		
		public static void RaisePlayerInputKeyDownEvent(KeyCode keyCode)
		{
			if (_playerInputKeyDown == null){
				LoggerService.LogWarning($"{nameof(ServerPlayerInputEvents)}::{nameof(RaisePlayerInputKeyDownEvent)} raised, but nothing picked it up");
				return;
			}
			_playerInputKeyDown.Invoke(keyCode);
		}
		
		public static void RaisePlayerInputKeyUpEvent(KeyCode keyCode)
		{
			if (_playerInputKeyUp == null){
				LoggerService.LogWarning($"{nameof(ServerPlayerInputEvents)}::{nameof(RaisePlayerInputKeyUpEvent)} raised, but nothing picked it up");
				return;
			}
			_playerInputKeyUp.Invoke(keyCode);
		}
		
		public static void RaisePlayerMobileTouchPositionEvent(Vector2 mobPosition)
		{
			if (_playerMobileTouchPosition == null){
				LoggerService.LogWarning($"{nameof(ServerPlayerInputEvents)}::{nameof(RaisePlayerMobileTouchPositionEvent)} raised, but nothing picked it up");
				return;
			}
			_playerMobileTouchPosition.Invoke(mobPosition);
		}
	}
}