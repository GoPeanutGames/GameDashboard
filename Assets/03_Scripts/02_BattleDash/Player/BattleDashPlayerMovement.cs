using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player
{
	public class BattleDashPlayerMovement : NetworkBehaviour
	{
		private Vector2 _movement = Vector2.zero;
		private float _speed = 5;

		private void OnEnable()
		{
			ServerPlayerInputEvents.PlayerInputKeyDown += OnPlayerInputKeyDown;
			ServerPlayerInputEvents.PlayerInputKeyUp += OnPlayerInputKeyUp;
		}

		private void Update()
		{
			if (IsServer){
				ServerUpdate();
			}
		}

		private void OnPlayerInputKeyDown(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyDown)}- press: {keyCode}");
			if (keyCode == KeyCode.A){
				_movement.x -= 1;
			}
			if (keyCode == KeyCode.D){
				_movement.x += 1;
			}
			if (keyCode == KeyCode.W){
				_movement.y += 1;
			}
			if (keyCode == KeyCode.S){
				_movement.y -= 1;
			}
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyDown)}- current movement: {_movement}");
		}

		private void OnPlayerInputKeyUp(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyUp)}- press: {keyCode}");
			if (keyCode == KeyCode.A){
				_movement.x += 1;
			}
			if (keyCode == KeyCode.D){
				_movement.x -= 1;
			}
			if (keyCode == KeyCode.W){
				_movement.y -= 1;
			}
			if (keyCode == KeyCode.S){
				_movement.y += 1;
			}
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyUp)}- current movement: {_movement}");
		}

		private void ServerUpdate()
		{
			this.transform.Translate(_movement * _speed * NetworkManager.ServerTime.FixedDeltaTime);
		}

		private void OnDisable()
		{
			ServerPlayerInputEvents.PlayerInputKeyDown -= OnPlayerInputKeyDown;
			ServerPlayerInputEvents.PlayerInputKeyUp -= OnPlayerInputKeyUp;
		}
	}
}