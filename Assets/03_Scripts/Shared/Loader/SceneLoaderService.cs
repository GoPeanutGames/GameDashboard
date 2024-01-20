using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;

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
			await AddressablesService.Instance.LoadAddressablesScene(sceneInfo);
			SceneLoaderEvents.Instance.RaiseSceneLoadedEvent();
		}

		private void OnSceneDownloadProgressUpdated(float progress)
		{
			SceneLoaderEvents.Instance.RaiseSceneLoadProgressUpdatedEvent(progress);
		}
	}
}