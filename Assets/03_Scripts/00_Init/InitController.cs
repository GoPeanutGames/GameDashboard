using System.Collections.Generic;
using Coffee.UIEffects;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PeanutDashboard.Init
{
	public class InitController : MonoBehaviour
	{
		//TODO: logs should be handled by a manager, that has a type, source, system so it can be easily filtered

		public SceneInfo dashboardScene;
		public Slider desktopDownloadProgressSlider;
		public Slider mobileDownloadProgressSlider;
		public List<UIDissolve> transitionEffects;
		public float transitionDuration = 1.2f;

		private void Start()
		{
			Debug.Log($"{nameof(InitController)}:: {nameof(Start)}");
			AddressablesEvents.Instance.addressablesInitialised += OnAddressablesInitialised;
			AddressablesService.Instance.InitialiseAddressables();
		}

		private void OnAddressablesInitialised()
		{
			SceneLoaderEvents.Instance.sceneLoaded += OnDashboardSceneLoaded;
			SceneLoaderEvents.Instance.sceneLoadProgressUpdated += OnSceneLoadProgressUpdate;
			SceneLoaderService.Instance.LoadScene(dashboardScene);
		}

		private void OnSceneLoadProgressUpdate(float value)
		{
			desktopDownloadProgressSlider.value = value;
			mobileDownloadProgressSlider.value = value;
		}
		
		private void OnDashboardSceneLoaded()
		{
			SceneLoaderEvents.Instance.sceneLoaded -= OnDashboardSceneLoaded;
			SceneLoaderEvents.Instance.sceneLoadProgressUpdated -= OnSceneLoadProgressUpdate;
			foreach (UIDissolve transitionEffect in transitionEffects)
			{
				transitionEffect.effectPlayer.duration = transitionDuration;
				transitionEffect.Play();
			}
			Invoke(nameof(OnTransitionDone), transitionDuration + 0.2f);
		}

		private void OnTransitionDone()
		{
			SceneManager.UnloadSceneAsync(0);
		}

		private void OnDestroy()
		{
			AddressablesEvents.Instance.addressablesInitialised -= OnAddressablesInitialised;
		}
	}
}