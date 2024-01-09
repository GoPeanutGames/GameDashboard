using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;

namespace PeanutDashboard.Init
{
	public class InitController : MonoBehaviour
	{
		//TODO: Dashboard should be a constant specified in a reference file
		//TODO: logs should be handled by a manager, that has a type, source, system so it can be easily filtered

		public TMP_Text downloadText;
		public Slider downloadProgressSlider;

		IEnumerator Start()
		{
			Debug.Log($"{nameof(InitController)}:: {nameof(Start)}");
			AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync();
			yield return handle;
			StartCoroutine(OnAddressablesInitialised());
		}

		private IEnumerator OnAddressablesInitialised()
		{
			Debug.Log($"{nameof(InitController)}::{nameof(OnAddressablesInitialised)}");
			AsyncOperationHandle<IList<IResourceLocation>> loadResourceLocationsAsync = Addressables.LoadResourceLocationsAsync("Dashboard", Addressables.MergeMode.Union);
			yield return loadResourceLocationsAsync;
			IList<IResourceLocation> resourceLocations = loadResourceLocationsAsync.Result;
			AsyncOperationHandle downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
			long totalBytes = downloadDependenciesAsync.GetDownloadStatus().TotalBytes;
			if (totalBytes > 0){
				float sizeInKb = totalBytes / 1024f;
				Debug.Log($"{nameof(InitController)}::{nameof(OnAddressablesInitialised)} - Downloading dashboard - size is {sizeInKb:F2} Kb");
				do{
					downloadText.text = "Downloading..." + Mathf.FloorToInt(downloadDependenciesAsync.GetDownloadStatus().DownloadedBytes / (float)totalBytes * 100) + "%";
					downloadProgressSlider.value = downloadDependenciesAsync.GetDownloadStatus().DownloadedBytes / (float)totalBytes;
					yield return null;
				} while (!downloadDependenciesAsync.IsDone);
			}
			else{
				Debug.LogWarning($"{nameof(InitController)}::{nameof(OnAddressablesInitialised)} - Download size is 0 for dashboard");
			}
			Addressables.LoadSceneAsync("DashboardScene");
		}
	}
}