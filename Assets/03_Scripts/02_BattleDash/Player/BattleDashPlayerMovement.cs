using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;
#if SERVER
using PeanutDashboard._02_BattleDash.Events;
#endif

namespace PeanutDashboard._02_BattleDash.Player
{
	public class BattleDashPlayerMovement : NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Animator _animator;

#if SERVER
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Vector2 _movement = Vector2.zero;

		[SerializeField]
		private Vector2 _currentMovement = Vector2.zero;

		[SerializeField]
		private float _currentSpeed;

		private const float Speed = 4;
		private const float AccelTime = 0.2f;
		private const float DecTime = 0.1f;
		private const float DirectionChangeTime = 0.1f;

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
		}

		private void Update()
		{
			if (IsServer){
				ServerUpdate();
			}
		}

		private void ServerUpdate()
		{
			_directionChangeTimer += NetworkManager.ServerTime.FixedDeltaTime;
			if (_directionChangeTimer < DirectionChangeTime){
				_currentMovement = Vector2.Lerp(_currentMovement, _movement, _directionChangeTimer / DirectionChangeTime);
			}

			if (_movement.magnitude > 0){
				_accelTimer += NetworkManager.ServerTime.FixedDeltaTime;
				_decTimer = 0f;
				if (_accelTimer < AccelTime){
					_currentSpeed = Mathf.Lerp(_currentSpeed, Speed, _accelTimer / AccelTime);
				}
			}
			else{
				_accelTimer = 0f;
				_decTimer += NetworkManager.ServerTime.FixedDeltaTime;
				if (_decTimer < DecTime){
					_currentSpeed = Mathf.Lerp(_currentSpeed, 0, _decTimer / DecTime);
				}
			}
			Vector2 velocity = _currentSpeed * _currentMovement;
			this.transform.Translate(velocity * NetworkManager.ServerTime.FixedDeltaTime);
			UpdateAnimator();
		}

		private void UpdateAnimator()
		{
			_animator.SetBool("Moving", _movement != Vector2.zero);
			_animator.SetFloat("MovementX", _currentMovement.x);
			_animator.SetFloat("MovementY", _currentMovement.y);
		}

		private void OnPlayerInputKeyDown(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyDown)}- press: {keyCode}");
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
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyDown)}- current movement: {_movement}");
		}

		private void OnPlayerInputKeyUp(KeyCode keyCode)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyUp)}- press: {keyCode}");
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
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPlayerInputKeyUp)}- current movement: {_movement}");
		}

		private void OnDisable()
		{
			ServerPlayerInputEvents.PlayerInputKeyDown -= OnPlayerInputKeyDown;
			ServerPlayerInputEvents.PlayerInputKeyUp -= OnPlayerInputKeyUp;
		}
#endif
	}
}