using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.User.Events
{
	public class UserEvents : Singleton<UserEvents>
	{
		private UnityAction<bool> _userLoggedIn;
		private UnityAction _userResourcesUpdated;
		private UnityAction _userGeneralInfoUpdated;
		
		public event UnityAction<bool> UserLoggedIn
		{
			add => _userLoggedIn += value;
			remove => _userLoggedIn -= value;
		}
		
		public event UnityAction UserResourcesUpdated
		{
			add => _userResourcesUpdated += value;
			remove => _userResourcesUpdated -= value;
		}
		
		public event UnityAction UserGeneralInfoUpdated
		{
			add => _userGeneralInfoUpdated += value;
			remove => _userGeneralInfoUpdated -= value;
		}

		public void RaiseUserLoggedInEvent(bool loggedIn)
		{
			if (_userLoggedIn == null){
				LoggerService.LogWarning($"{nameof(UserEvents)}::{nameof(RaiseUserLoggedInEvent)} raised, but nothing picked it up");
				return;
			}
			_userLoggedIn.Invoke(loggedIn);
		}

		public void RaiseUserResourcesUpdatedEvent()
		{
			if (_userResourcesUpdated == null){
				LoggerService.LogWarning($"{nameof(UserEvents)}::{nameof(RaiseUserResourcesUpdatedEvent)} raised, but nothing picked it up");
				return;
			}
			_userResourcesUpdated.Invoke();
		}

		public void RaiseUserGeneralInfoUpdatedEvent()
		{
			if (_userGeneralInfoUpdated == null){
				LoggerService.LogWarning($"{nameof(UserEvents)}::{nameof(RaiseUserGeneralInfoUpdatedEvent)} raised, but nothing picked it up");
				return;
			}
			_userGeneralInfoUpdated.Invoke();
		}
	}
}