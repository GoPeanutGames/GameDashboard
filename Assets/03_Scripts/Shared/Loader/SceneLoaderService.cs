using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared
{
    public class SceneLoaderService : Singleton<SceneLoaderService>
    {
        public async void LoadScene(SceneInfo sceneInfo)
        {
            AddressablesEvents.Instance.downloadPercentageUpdated += OnSceneDownloadProgressUpdated;
            await AddressablesService.Instance.DownloadAddressablesForScene(sceneInfo);
            AddressablesEvents.Instance.downloadPercentageUpdated -= OnSceneDownloadProgressUpdated;
            await AddressablesService.Instance.LoadAddressablesScene(sceneInfo);
            SceneLoaderEvents.Instance.sceneLoaded.Invoke();
        }

        private void OnSceneDownloadProgressUpdated(float progress)
        {
            SceneLoaderEvents.Instance.sceneLoadProgressUpdated.Invoke(progress);
        }
    }
}
