using System.Collections.Generic;
using Coffee.UIEffects;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard.Init
{
	public class InitController : MonoBehaviour
	{
		public SceneInfo dashboardScene;
		public Slider desktopDownloadProgressSlider;
		public Slider mobileDownloadProgressSlider;
		public List<UIDissolve> transitionEffects;
		public float transitionDuration = 1.2f;

		private void Start()
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(Start)}");
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(Start)}:: version: {Application.version}");
			AddressablesEvents.Instance.AddressablesInitialised += OnAddressablesInitialised;
			AddressablesService.Instance.InitialiseAddressables();
		}

		private void OnAddressablesInitialised()
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(OnAddressablesInitialised)}");
			SceneLoaderEvents.Instance.SceneLoaded += OnDashboardSceneLoaded;
			SceneLoaderEvents.Instance.SceneLoadProgressUpdated += OnSceneLoadProgressUpdate;
			SceneLoaderService.Instance.LoadScene(dashboardScene);
		}

		private void OnSceneLoadProgressUpdate(float value)
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(OnSceneLoadProgressUpdate)} - percentage: {value}");
			desktopDownloadProgressSlider.value = value;
			mobileDownloadProgressSlider.value = value;
		}

		private void OnDashboardSceneLoaded()
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(OnDashboardSceneLoaded)}");
			SceneLoaderEvents.Instance.SceneLoaded -= OnDashboardSceneLoaded;
			SceneLoaderEvents.Instance.SceneLoadProgressUpdated -= OnSceneLoadProgressUpdate;
			foreach (UIDissolve transitionEffect in transitionEffects){
				transitionEffect.effectPlayer.duration = transitionDuration;
				transitionEffect.Play();
			}
			Invoke(nameof(OnTransitionDone), transitionDuration + 0.2f);
		}

		private void OnTransitionDone()
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(OnTransitionDone)}");
			SceneManager.UnloadSceneAsync(0);
		}

		private void OnDestroy()
		{
			LoggerService.LogInfo($"{nameof(InitController)}::{nameof(OnDestroy)}");
			AddressablesEvents.Instance.AddressablesInitialised -= OnAddressablesInitialised;
		}
	}
}