using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;
#if SERVER
using PeanutDashboard.Utils.Math;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.State;
#endif

namespace PeanutDashboard._02_BattleDash.Player.Server
{
	public class ServerBattleDashPlayerMovement : NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Animator _animator;

#if SERVER
		private Vector2 _movement = Vector2.zero;

		private Vector2 _currentMovement = Vector2.zero;

		private float _currentSpeed;

		private float _directionChangeTimer = 0f;
		private float _accelTimer = 0f;
		private float _decTimer = 0f;

		private bool _isAPressed;
		private bool _isWPressed;
		private bool _isDPressed;
		private bool _isSPressed;

		private void OnEnable()
		{
			ServerPlayerInputEvents.PlayerInputKeyDown += OnPlayerInputKeyDown;
			ServerPlayerInputEvents.PlayerInputKeyUp += OnPlayerInputKeyUp;
			ServerPlayerInputEvents.PlayerMobileTouchPosition += OnPlayerMobileTouchPosition;
		}

		private void Update()
		{
			ServerUpdate();
		}

		private void ServerUpdate()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			_directionChangeTimer += NetworkManager.ServerTime.FixedDeltaTime;
			if (_directionChangeTimer < BattleDashConfig.MovementDirectionChangeTime){
				_currentMovement =
					Vector2.Lerp(_currentMovement, _movement, _directionChangeTimer / BattleDashConfig.MovementDirectionChangeTime);
			}

			if (_movement.magnitude > 0){
				_accelTimer += NetworkManager.ServerTime.FixedDeltaTime;
				_decTimer = 0f;
				if (_accelTimer < BattleDashConfig.MovementAccelTime){
					_currentSpeed =
						Mathf.Lerp(_currentSpeed, BattleDashConfig.MovementSpeed, _accelTimer / BattleDashConfig.MovementAccelTime);
				}
			}
			else{
				_accelTimer = 0f;
				_decTimer += NetworkManager.ServerTime.FixedDeltaTime;
				if (_decTimer < BattleDashConfig.MovementDecTime){
					_currentSpeed = Mathf.Lerp(_currentSpeed, 0, _decTimer / BattleDashConfig.MovementDecTime);
				}
				else{
					_currentSpeed = 0;
				}
			}
			Vector2 velocity = _currentSpeed * _currentMovement;
			this.transform.Translate(velocity * NetworkManager.ServerTime.FixedDeltaTime);
			this.transform.position = new Vector3(
				Mathf.Clamp(this.transform.position.x, -42, -20),
				Mathf.Clamp(this.transform.position.y, -22, 20),
				0);
			UpdateAnimator();
		}

		private void UpdateAnimator()
		{
			_animator.SetBool("Moving", _movement != Vector2.zero);
			_animator.SetFloat("MovementX", _currentMovement.x);
			_animator.SetFloat("MovementY", _currentMovement.y);
		}

		private void OnPlayerMobileTouchPosition(Vector2 worldTouchPos)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerMobileTouchPosition)}- press: {worldTouchPos}");
			Vector2 direction = worldTouchPos - this.transform.position.ToVector2();
			_movement = new Vector2(Mathf.Clamp(direction.normalized.x * 2f, -1, 1), Mathf.Clamp(direction.normalized.y * 2f, -1, 1));
			if (_currentMovement != _movement){
				_directionChangeTimer = 0;
			}
			if (worldTouchPos == Vector2.zero){
				_movement = Vector2.zero;
			}
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerMobileTouchPosition)}- current movement: {_movement}");
		}

		private void OnPlayerInputKeyDown(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerInputKeyDown)}- press: {keyCode}");
			_directionChangeTimer = 0;
			switch (keyCode){
				case KeyCode.A:
					_isAPressed = true;
					_movement.x = -1;
					break;
				case KeyCode.D:
					_isDPressed = true;
					_movement.x = 1;
					break;
				case KeyCode.W:
					_isWPressed = true;
					_movement.y = 1;
					break;
				case KeyCode.S:
					_isSPressed = true;
					_movement.y = -1;
					break;
			}
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerInputKeyDown)}- current movement: {_movement}");
		}

		private void OnPlayerInputKeyUp(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerInputKeyUp)}- press: {keyCode}");
			_directionChangeTimer = 0;
			switch (keyCode){
				case KeyCode.A:
					_isAPressed = false;
					_movement.x = _isDPressed ? 1 : 0;
					break;
				case KeyCode.D:
					_isDPressed = false;
					_movement.x = _isAPressed ? -1 : 0;
					break;
				case KeyCode.W:
					_isWPressed = false;
					_movement.y = _isSPressed ? -1 : 0;
					break;
				case KeyCode.S:
					_isSPressed = false;
					_movement.y = _isWPressed ? 1 : 0;
					break;
			}
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerMovement)}::{nameof(OnPlayerInputKeyUp)}- current movement: {_movement}");
		}

		private void OnDisable()
		{
			ServerPlayerInputEvents.PlayerInputKeyDown -= OnPlayerInputKeyDown;
			ServerPlayerInputEvents.PlayerInputKeyUp -= OnPlayerInputKeyUp;
			ServerPlayerInputEvents.PlayerMobileTouchPosition -= OnPlayerMobileTouchPosition;
		}
#else
	    private void Update()
	    {
		    ClientActionEvents.RaiseUpdatePlayerVisualPositionEvent(this.transform.position);
	    }
#endif
	}
}