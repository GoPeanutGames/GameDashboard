using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
#if !SERVER && !UNITY_EDITOR
using PeanutDashboard.Utils.WebGL;
#endif
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashPlayerController : NetworkBehaviour
	{
		private readonly NetworkVariable<Vector2> _mobileTouchMove = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
		
		private bool _isMobile;
		
		private void OnEnable()
		{
			ServerSpawnEvents.SpawnedPlayerVisual += ServerSpawnedVisual;
#if SERVER
			_mobileTouchMove.OnValueChanged += ServerOnMobileTouchChanged;
#endif
#if !SERVER && !UNITY_EDITOR
			_isMobile = WebGLUtils.IsWebMobile;
			if (_isMobile)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
#endif
		}

		private void ServerSpawnedVisual(GameObject visual)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(ServerSpawnedVisual)}");
			visual.GetComponent<NetworkObject>().Spawn();
			visual.GetComponent<NetworkObject>().TrySetParent(this.transform);
			visual.transform.localPosition = new Vector3(-20,0,0);
		}

		private void Update()
		{
#if !SERVER
				if (_isMobile)
				{
					CheckForMobileInput();
				}
				else
				{
					CheckForDesktopInput();
				}
#endif
		}

		private void CheckForMobileInput()
		{
			bool moveChanged = false;
			if (Input.touchCount > 0){
				for (int i = 0; i < Input.touchCount; i++)
				{
					Touch touch = Input.GetTouch(0);
					if (touch.position.x <= Screen.width / 3){
						Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
						touchPosition.z = 0;
						_mobileTouchMove.Value = touchPosition;
						moveChanged = true;
					}
					else{
						//TODO: send mobile reticule event
					}
				}
			}
			if (!moveChanged){
				_mobileTouchMove.Value = Vector2.zero;
			}
		}

		private void ServerOnMobileTouchChanged(Vector2 prevMobileTouch, Vector2 newMobileTouch)
		{
			ServerPlayerInputEvents.RaisePlayerMobileTouchPositionEvent(newMobileTouch);
		}

		private void CheckForDesktopInput()
		{
			if (Input.GetKeyDown(KeyCode.A)){
				SendPlayerKeyDown_ServerRpc(KeyCode.A);
			}
			else if (Input.GetKeyDown(KeyCode.D)){
				SendPlayerKeyDown_ServerRpc(KeyCode.D);
			}
			else if (Input.GetKeyDown(KeyCode.W)){
				SendPlayerKeyDown_ServerRpc(KeyCode.W);
			}
			else if (Input.GetKeyDown(KeyCode.S)){
				SendPlayerKeyDown_ServerRpc(KeyCode.S);
			}
			if (Input.GetKeyUp(KeyCode.A)){
				SendPlayerKeyUp_ServerRpc(KeyCode.A);
			}
			else if (Input.GetKeyUp(KeyCode.D)){
				SendPlayerKeyUp_ServerRpc(KeyCode.D);
			}
			else if (Input.GetKeyUp(KeyCode.W)){
				SendPlayerKeyUp_ServerRpc(KeyCode.W);
			}
			else if (Input.GetKeyUp(KeyCode.S)){
				SendPlayerKeyUp_ServerRpc(KeyCode.S);
			}
		}

		private void OnDisable()
		{
#if SERVER
			_mobileTouchMove.OnValueChanged -= ServerOnMobileTouchChanged;
#endif
#if !SERVER && !UNITY_EDITOR
			if (_isMobile)
			{
				Screen.orientation = ScreenOrientation.Portrait;
			}
#endif
			ServerSpawnEvents.SpawnedPlayerVisual -= ServerSpawnedVisual;
		}

		[ServerRpc]
		private void SendPlayerKeyDown_ServerRpc(KeyCode keyCode)
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashPlayerController)}::{nameof(SendPlayerKeyDown_ServerRpc)}- press: {keyCode}");
			ServerPlayerInputEvents.RaisePlayerInputKeyDownEvent(keyCode);
		}

		[ServerRpc]
		private void SendPlayerKeyUp_ServerRpc(KeyCode keyCode)
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashPlayerController)}::{nameof(SendPlayerKeyUp_ServerRpc)}- press: {keyCode}");
			ServerPlayerInputEvents.RaisePlayerInputKeyUpEvent(keyCode);
		}
	}
}