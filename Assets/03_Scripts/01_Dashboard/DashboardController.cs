using System.Collections;
using System.Collections.Generic;
using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.User.Events;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace PeanutDashboard.Dashboard
{
	public class DashboardController : MonoBehaviour
	{
		private void Start()
		{
			UserEvents.Instance.UserLoggedIn += OnUserLoggedIn;
		}

		public void OpenGameButtonClick(SceneInfo sceneInfo)
		{
			StartCoroutine(DownloadAndStartGame(sceneInfo));
		}

		private IEnumerator DownloadAndStartGame(SceneInfo sceneInfo)
		{
			AsyncOperationHandle<IList<IResourceLocation>> loadResourceLocationsAsync = Addressables.LoadResourceLocationsAsync(sceneInfo.label, Addressables.MergeMode.Union);
			yield return loadResourceLocationsAsync;
			IList<IResourceLocation> resourceLocations = loadResourceLocationsAsync.Result;
			AsyncOperationHandle downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
			yield return downloadDependenciesAsync;
			Addressables.LoadSceneAsync(sceneInfo.name);
		}

		private void OnUserLoggedIn(bool loggedIn)
		{
			DashboardUIEvents.Instance.RaiseShowLogInUIEvent(loggedIn);
		}

		private void OnDestroy()
		{
			UserEvents.Instance.UserLoggedIn -= OnUserLoggedIn;
		}
	}
}