using System.Collections.Generic;
using System.Threading.Tasks;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace PeanutDashboard.Shared
{
	public class AddressablesService : Singleton<AddressablesService>
	{
		private bool _addressablesInitialised = false;
		
		public async void InitialiseAddressables()
		{
			LoggerService.LogInfo($"{nameof(AddressablesService)}::{nameof(InitialiseAddressables)}");
			await Addressables.InitializeAsync().Task;
			_addressablesInitialised = true;
			AddressablesEvents.Instance.RaiseAddressablesInitialisedEvent();
		}

		public async Task DownloadAddressablesForScene(SceneInfo sceneInfo)
		{
			LoggerService.LogInfo($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)}");
			long getDownloadSize = await Addressables.GetDownloadSizeAsync(sceneInfo.label).Task;
			if (getDownloadSize > 0){
				IList<IResourceLocation> resourceLocations = await Addressables.LoadResourceLocationsAsync(sceneInfo.label).Task;
				AsyncOperationHandle downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
				while (!downloadDependenciesAsync.IsDone){
					LoggerService.LogInfo($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)} - {downloadDependenciesAsync.PercentComplete}");
					LoggerService.LogInfo($"{nameof(AddressablesService)}::{nameof(DownloadAddressablesForScene)} - bytes: {downloadDependenciesAsync.GetDownloadStatus().DownloadedBytes}");
					AddressablesEvents.Instance.RaiseDownloadPercentageUpdatedEvent(downloadDependenciesAsync.PercentComplete);
					await Task.Yield();
				}
				Addressables.Release(downloadDependenciesAsync);
			}
		}

		public async Task LoadAddressablesScene(SceneInfo sceneInfo, LoadSceneMode loadSceneMode)
		{
			LoggerService.LogInfo($"{nameof(AddressablesService)}::{nameof(LoadAddressablesScene)} - {sceneInfo.name}");
			await Addressables.LoadSceneAsync(sceneInfo.key, loadSceneMode).Task;
		}

		public bool AreAddressablesInitialised()
		{
			return _addressablesInitialised;
		}
	}
}