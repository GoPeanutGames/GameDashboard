using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public static class LoadingEvents
	{
		private static UnityAction<string> _showLoading;
		private static UnityAction<string> _updateLoading;
		private static UnityAction _hideLoading;
		
		public static event UnityAction<string> ShowLoading
		{
			add => _showLoading += value;
			remove => _showLoading -= value;
		}
		
		public static event UnityAction<string> UpdateLoading
		{
			add => _updateLoading += value;
			remove => _updateLoading -= value;
		}
		
		public static event UnityAction HideLoading
		{
			add => _hideLoading += value;
			remove => _hideLoading -= value;
		}
		
		public static void RaiseShowLoadingEvent(string loadingText)
		{
			if (_showLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseShowLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_showLoading.Invoke(loadingText);
		}
		
		public static void RaiseUpdateLoadingEvent(string loadingText)
		{
			if (_updateLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseUpdateLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_updateLoading.Invoke(loadingText);
		}
		
		public static void RaiseHideLoadingEvent()
		{
			if (_hideLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseHideLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_hideLoading.Invoke();
		}
	}
}