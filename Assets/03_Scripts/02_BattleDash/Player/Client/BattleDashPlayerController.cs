using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
#if !SERVER
using PeanutDashboard._02_BattleDash.State;
using PeanutDashboard.Utils.WebGL;
#endif
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashPlayerController : NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _audioClip;
		
		private readonly NetworkVariable<Vector2> _mobileTouchMove = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

		private void OnEnable()
		{
#if SERVER
			ServerSpawnEvents.SpawnedPlayerVisual += ServerSpawnedVisual;
			_mobileTouchMove.OnValueChanged += ServerOnMobileTouchMoveChanged;
#endif
#if !SERVER
			if (WebGLUtils.IsWebMobile){
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			BattleDashClientUIEvents.OnShowTooltips += OnShowTooltips;
			BattleDashClientUIEvents.OnOpenEndGamePopup += OnPauseGame;
			BattleDashClientUIEvents.OnShowGameOver += OnPauseGame;
			BattleDashClientUIEvents.OnShowWon += OnPauseGame;
			BattleDashClientUIEvents.OnHideTooltips += OnUnpauseGame;
			BattleDashClientUIEvents.OnCloseEndGamePopup += OnUnpauseGame;
			ClientActionEvents.OnPlayerRequestDisconnect += OnRequestDisconnect;
#endif
		}

		private void OnRequestDisconnect()
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnRequestDisconnect)}");
			SendRequestDisconnect_ServerRpc();
		}

		private void OnShowTooltips(bool _)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnShowTooltips)}");
			OnPauseGame();
		}
		
		private void OnPauseGame()
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnPauseGame)}");
			ClientBattleDashAudioEvents.RaiseFadeOutMusicEvent(1f);
			SendPlayerPaused_ServerRpc();
		}

		private void OnUnpauseGame()
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(OnUnpauseGame)}");
			ClientBattleDashAudioEvents.RaiseFadeInMusicEvent(_audioClip);
			SendPlayerUnPaused_ServerRpc();
		}
		
		private void ServerSpawnedVisual(GameObject visual)
		{
			LoggerService.LogInfo($"{nameof(BattleDashPlayerController)}::{nameof(ServerSpawnedVisual)}");
			visual.GetComponent<NetworkObject>().Spawn();
			visual.GetComponent<NetworkObject>().TrySetParent(this.transform);
			visual.transform.localPosition = new Vector3(-20, 0, 0);
		}

#if !SERVER
		private void Update()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			if (WebGLUtils.IsWebMobile){
				CheckForMobileInput();
			}
			else{
				CheckForDesktopInput();
			}
		}
#endif

		private void CheckForMobileInput()
		{
			bool moveChanged = false;
			if (Input.touchCount > 0){
				for (int i = 0; i < Input.touchCount; i++){
					Touch touch = Input.GetTouch(i);
					if (touch.position.x <= Screen.width / 3f){
						Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
						touchPosition.z = 0;
						_mobileTouchMove.Value = touchPosition;
						if (touch.phase == TouchPhase.Ended){
							_mobileTouchMove.Value = Vector2.zero;
						}
						moveChanged = true;
					}
					else{
						ClientActionEvents.RaiseMobilePlayerTouchShootPositionEvent(touch.position);
					}
				}
			}
			if (!moveChanged){
				_mobileTouchMove.Value = Vector2.zero;
			}
		}

		private void ServerOnMobileTouchMoveChanged(Vector2 prevMobileTouch, Vector2 newMobileTouch)
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
			_mobileTouchMove.OnValueChanged -= ServerOnMobileTouchMoveChanged;
			ServerSpawnEvents.SpawnedPlayerVisual -= ServerSpawnedVisual;
#endif
#if !SERVER
			if (WebGLUtils.IsWebMobile){
				Screen.orientation = ScreenOrientation.Portrait;
			}
			BattleDashClientUIEvents.OnShowTooltips -= OnShowTooltips;
			BattleDashClientUIEvents.OnOpenEndGamePopup -= OnPauseGame;
			BattleDashClientUIEvents.OnShowGameOver -= OnPauseGame;
			BattleDashClientUIEvents.OnShowWon -= OnPauseGame;
			BattleDashClientUIEvents.OnHideTooltips -= OnUnpauseGame;
			BattleDashClientUIEvents.OnCloseEndGamePopup -= OnUnpauseGame;
			ClientActionEvents.OnPlayerRequestDisconnect -= OnRequestDisconnect;
#endif
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

		[ServerRpc]
		private void SendPlayerPaused_ServerRpc()
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashPlayerController)}::{nameof(SendPlayerPaused_ServerRpc)}");
			ServerGameStateEvents.RaisePauseTriggeredEvent();
		}

		[ServerRpc]
		private void SendPlayerUnPaused_ServerRpc()
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashPlayerController)}::{nameof(SendPlayerUnPaused_ServerRpc)}");
			ServerGameStateEvents.RaiseUnPauseTriggeredEvent();
		}
		
		[ServerRpc]
		private void SendRequestDisconnect_ServerRpc()
		{
			LoggerService.LogInfo($"[SERVER-RPC]{nameof(BattleDashPlayerController)}::{nameof(SendRequestDisconnect_ServerRpc)}");
			NetworkManager.Shutdown();
		}
	}
}