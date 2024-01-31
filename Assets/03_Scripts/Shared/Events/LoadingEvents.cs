using MetaMask.Unity;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public class LoadingEvents: Singleton<LoadingEvents>
	{
		private UnityAction<string> _showLoading;
		private UnityAction<string> _updateLoading;
		private UnityAction _hideLoading;
		
		public event UnityAction<string> ShowLoading
		{
			add => _showLoading += value;
			remove => _showLoading -= value;
		}
		
		public event UnityAction<string> UpdateLoading
		{
			add => _updateLoading += value;
			remove => _updateLoading -= value;
		}
		
		public event UnityAction HideLoading
		{
			add => _hideLoading += value;
			remove => _hideLoading -= value;
		}
		
		public void RaiseShowLoadingEvent(string loadingText)
		{
			if (_showLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseShowLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_showLoading.Invoke(loadingText);
		}
		
		public void RaiseUpdateLoadingEvent(string loadingText)
		{
			if (_updateLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseUpdateLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_updateLoading.Invoke(loadingText);
		}
		
		public void RaiseHideLoadingEvent()
		{
			if (_hideLoading == null){
				LoggerService.LogWarning($"{nameof(SceneLoaderEvents)}::{nameof(RaiseHideLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_hideLoading.Invoke();
		}
	}
}