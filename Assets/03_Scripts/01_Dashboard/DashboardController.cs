using System.Collections;
using System.Collections.Generic;
using PeanutDashboard.Init;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace PeanutDashboard.Dashboard
{
	public class DashboardController : MonoBehaviour
	{
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
	}
}