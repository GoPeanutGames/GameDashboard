using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Init;
using PeanutDashboard.Shared;
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
		private void OnEnable()
		{
			UserEvents.Instance.UserLoggedIn += OnUserLoggedIn;
			SceneLoaderEvents.Instance.LoadAndOpenScene += OnStartGame;
		}

		private void Start()
		{
			AuthenticationService.Initialise();
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