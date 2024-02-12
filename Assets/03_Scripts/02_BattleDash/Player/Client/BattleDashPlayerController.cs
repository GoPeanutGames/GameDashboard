using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.WebGL;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashPlayerController : NetworkBehaviour
	{
		private void OnEnable()
		{
			ServerSpawnEvents.SpawnedPlayerVisual += ServerSpawnedVisual;
#if !SERVER
			if (WebGLUtils.IsMobile)
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
			if (IsClient){
				if (WebGLUtils.IsMobile)
				{
					CheckForMobileInput();
				}
				else
				{
					CheckForDesktopInput();
				}
			}
		}

		private void CheckForMobileInput()
		{
			if (Input.touchCount > 0)
			{
				for (int i = 0; i < Input.touchCount; i++)
				{
					Touch touch = Input.GetTouch(0);
					//TODO: check if touch position is on the left or right
					//TODO: if left -> movement
					//TODO: if right -> move reticule / target
				}
			}
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
#if !SERVER
			if (WebGLUtils.IsMobile)
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