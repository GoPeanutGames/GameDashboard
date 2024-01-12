using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared
{
    public class SceneLoaderService : Singleton<SceneLoaderService>
    {
        public async void LoadScene(SceneInfo sceneInfo)
        {
            Debug.Log($"{nameof(SceneLoaderService)}::{nameof(LoadScene)} - loading scene {sceneInfo.name}");
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
