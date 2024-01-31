using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Config;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Metamask;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard.Dashboard
{
	public class DashboardController : MonoBehaviour
	{
		private void OnEnable()
		{
			UserEvents.Instance.UserLoggedIn += OnUserLoggedIn;
			SceneLoaderEvents.Instance.LoadAndOpenScene += OnStartGame;
			CheckAndRemoveServerConnection();
		}

		private void CheckAndRemoveServerConnection()
		{
			if (NetworkManager.Singleton != null){
				NetworkManager.Singleton.Shutdown();
				Destroy(NetworkManager.Singleton.gameObject);
			}
		}

		private void Start()
		{
			GameConfig gameConfig = EnvironmentManager.Instance.GetGameConfig();
			AuthenticationService.Initialise(gameConfig.currentMetaMaskConfig, gameConfig.currentEnvironmentModel.unityEnvironmentName);
			if (UserService.Instance.IsLoggedIn()){
				OnUserLoggedIn(true);
			}
		}

		private void OnStartGame(SceneInfo sceneInfo)
		{
			LoggerService.LogInfo($"{nameof(DashboardController)}::{nameof(OnStartGame)} - {sceneInfo.key}");
			SceneLoaderService.Instance.LoadAndOpenScene(sceneInfo);
		}

		private void OnUserLoggedIn(bool loggedIn)
		{
			LoggerService.LogInfo($"{nameof(DashboardController)}::{nameof(OnUserLoggedIn)} - {loggedIn}");
			DashboardUIEvents.Instance.RaiseShowLogInUIEvent(loggedIn);
		}

		private void OnDisable()
		{
			SceneLoaderEvents.Instance.LoadAndOpenScene -= OnStartGame;
			UserEvents.Instance.UserLoggedIn -= OnUserLoggedIn;
		}
	}
}