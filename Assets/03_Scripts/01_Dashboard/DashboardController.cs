using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace PeanutDashboard.Dashboard
{
	public class DashboardController : MonoBehaviour
	{
		public void OpenGameButtonClick(string gameScene)
		{
			GameScenes scene = gameScene == "Game1" ? GameScenes.Game1 : GameScenes.Game2;
			StartCoroutine(DownloadAndStartGame(scene));
		}

		private IEnumerator DownloadAndStartGame(GameScenes gameScenes)
		{
			string labelName = gameScenes == GameScenes.Game1 ? "Game1" : "Game2";
			string sceneName = gameScenes == GameScenes.Game1 ? "Game1Scene" : "Game2Scene";
			AsyncOperationHandle<IList<IResourceLocation>> loadResourceLocationsAsync = Addressables.LoadResourceLocationsAsync(labelName, Addressables.MergeMode.Union);
			yield return loadResourceLocationsAsync;
			IList<IResourceLocation> resourceLocations = loadResourceLocationsAsync.Result;
			AsyncOperationHandle downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
			yield return downloadDependenciesAsync;
			Addressables.LoadSceneAsync(sceneName);
		}
	}

	public enum GameScenes
	{
		Game1,
		Game2
	}
}