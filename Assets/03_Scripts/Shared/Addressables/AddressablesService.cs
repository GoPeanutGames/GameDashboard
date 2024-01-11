using System.Collections.Generic;
using System.Threading.Tasks;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace PeanutDashboard.Shared
{
    public class AddressablesService: Singleton<AddressablesService>
    {
        public async void InitialiseAddressables()
        {
            Debug.Log($"{nameof(AddressablesService)}::{nameof(InitialiseAddressables)}");
            await Addressables.InitializeAsync().Task;
            AddressablesEvents.Instance.addressablesInitialised.Invoke();
        }

        public async Task DownloadAddressablesForScene(SceneInfo sceneInfo)
        {
            Debug.Log($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)}");
            IList<IResourceLocation> resourceLocations = await Addressables.LoadResourceLocationsAsync(sceneInfo.label, Addressables.MergeMode.Union).Task;
            AsyncOperationHandle downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
            long totalBytes = downloadDependenciesAsync.GetDownloadStatus().TotalBytes;
            if (totalBytes > 0){
                float sizeInKb = totalBytes / 1024f;
                Debug.Log($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)} - Downloading {sceneInfo.name} - size is {sizeInKb:F2} Kb");
                do{
                    AddressablesEvents.Instance.downloadPercentageUpdated.Invoke(downloadDependenciesAsync.GetDownloadStatus().DownloadedBytes / (float)totalBytes);
                } while (!downloadDependenciesAsync.IsDone);
            }
            else{
                Debug.LogWarning($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)} - Download size is 0 for {sceneInfo.name}");
            }
        }

        public async Task LoadAddressablesScene(SceneInfo sceneInfo)
        {
            await Addressables.LoadSceneAsync(sceneInfo.name, LoadSceneMode.Additive).Task;
        }
    }
}