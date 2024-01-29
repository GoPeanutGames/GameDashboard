using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Config;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Metamask;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;
using UnityEngine;

namespace PeanutDashboard.Dashboard
{
	public class DashboardController : MonoBehaviour
	{
		[Header("Set In Inspector")]
		[SerializeField]
		private GameConfig _gameConfig;
		
		private void OnEnable()
		{
			UserEvents.Instance.UserLoggedIn += OnUserLoggedIn;
			SceneLoaderEvents.Instance.LoadAndOpenScene += OnStartGame;
		}

		private void Start()
		{
			
			AuthenticationService.Initialise(_gameConfig.currentMetaMaskConfig, _gameConfig.currentEnvironmentModel.unityEnvironmentName);
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