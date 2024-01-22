using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.SceneManagement;

namespace PeanutDashboard.Shared
{
	public class SceneLoaderService : Singleton<SceneLoaderService>
	{
		public async void LoadScene(SceneInfo sceneInfo)
		{
			LoggerService.LogInfo($"{nameof(SceneLoaderService)}::{nameof(LoadScene)} - loading scene {sceneInfo.name}");
			AddressablesEvents.Instance.DownloadPercentageUpdated += OnSceneDownloadProgressUpdated;
			await AddressablesService.Instance.DownloadAddressablesForScene(sceneInfo);
			AddressablesEvents.Instance.DownloadPercentageUpdated -= OnSceneDownloadProgressUpdated;
			await AddressablesService.Instance.LoadAddressablesScene(sceneInfo, LoadSceneMode.Additive);
			SceneLoaderEvents.Instance.RaiseSceneLoadedEvent();
		}

		public async void LoadAndOpenScene(SceneInfo sceneInfo)
		{
			LoggerService.LogInfo($"{nameof(SceneLoaderService)}::{nameof(LoadAndOpenScene)} - loading scene {sceneInfo.name}");
			AddressablesEvents.Instance.DownloadPercentageUpdated += OnSceneDownloadProgressUpdated;
			await AddressablesService.Instance.DownloadAddressablesForScene(sceneInfo);
			AddressablesEvents.Instance.DownloadPercentageUpdated -= OnSceneDownloadProgressUpdated;
			await AddressablesService.Instance.LoadAddressablesScene(sceneInfo, LoadSceneMode.Single);
		}

		private void OnSceneDownloadProgressUpdated(float progress)
		{
			SceneLoaderEvents.Instance.RaiseSceneLoadProgressUpdatedEvent(progress);
		}
	}
}