using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public class SceneLoaderEvents : Singleton<SceneLoaderEvents>
	{
		private UnityAction<float> _sceneLoadProgressUpdated;
		private UnityAction _sceneLoaded;
		
		public event UnityAction<float> SceneLoadProgressUpdated
		{
			add => _sceneLoadProgressUpdated += value;
			remove => _sceneLoadProgressUpdated -= value;
		}
		
		public event UnityAction SceneLoaded
		{
			add => _sceneLoaded += value;
			remove => _sceneLoaded -= value;
		}
		
		public void RaiseSceneLoadProgressUpdatedEvent(float progress)
		{
			if (_sceneLoadProgressUpdated == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseSceneLoadProgressUpdatedEvent)} raised, but nothing picked it up");
				return;
			}
			_sceneLoadProgressUpdated.Invoke(progress);
		}
		
		public void RaiseSceneLoadedEvent()
		{
			if (_sceneLoaded == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseSceneLoadedEvent)} raised, but nothing picked it up");
				return;
			}
			_sceneLoaded.Invoke();
		}
	}
}