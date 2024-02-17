using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UnityServer
{
	public class BattleDashDisconnect: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private SceneInfo _sceneInfo;
		
#if !SERVER
		private void OnEnable()
		{
			NetworkManager.Singleton.OnClientStopped += OnClientStopped;
		}

		private void OnDisable()
		{
			if (NetworkManager.Singleton != null){
				NetworkManager.Singleton.OnClientStopped -= OnClientStopped;
			}
		}

		private void OnClientStopped(bool stopped)
		{
			Debug.Log($"{nameof(BattleDashDisconnect)}::{nameof(OnClientStopped)}");
			NetworkManager.Singleton.Shutdown();
			Destroy(NetworkManager.Singleton.gameObject);
			Time.timeScale = 1;
			SceneLoaderService.Instance.LoadAndOpenScene(_sceneInfo);
		}
#endif
	}
}