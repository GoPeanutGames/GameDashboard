using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
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
			NetworkManager.Singleton.OnClientStopped += OnClientDisconnected;
		}

		private void OnDisable()
		{
			NetworkManager.Singleton.OnClientStopped -= OnClientDisconnected;
		}

		private void OnClientDisconnected(bool id)
		{
			Debug.Log($"{nameof(BattleDashDisconnect)}::{nameof(OnClientDisconnected)}");
			//TODO: this is called, but scene is not loaded
			SceneLoaderEvents.Instance.RaiseLoadAndOpenSceneEvent(_sceneInfo);
		}
#endif
	}
}