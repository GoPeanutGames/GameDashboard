using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ServerPlayerInputEvents
	{
		private static UnityAction<KeyCode> _playerInputKeyDown;
		private static UnityAction<KeyCode> _playerInputKeyUp;
		
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
		
		public static void RaisePlayerInputKeyDownEvent(KeyCode keyCode)
		{
			if (_playerInputKeyDown == null){
				LoggerService.LogWarning($"{nameof(ServerSpawnEvents)}::{nameof(RaisePlayerInputKeyDownEvent)} raised, but nothing picked it up");
				return;
			}
			_playerInputKeyDown.Invoke(keyCode);
		}
		
		public static void RaisePlayerInputKeyUpEvent(KeyCode keyCode)
		{
			if (_playerInputKeyUp == null){
				LoggerService.LogWarning($"{nameof(ServerSpawnEvents)}::{nameof(RaisePlayerInputKeyUpEvent)} raised, but nothing picked it up");
				return;
			}
			_playerInputKeyUp.Invoke(keyCode);
		}
	}
}