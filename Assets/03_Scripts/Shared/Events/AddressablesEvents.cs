using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public class AddressablesEvents : Singleton<AddressablesEvents>
	{
		private UnityAction _addressablesInitialised;
		private UnityAction<float> _downloadPercentageUpdated;

		public event UnityAction AddressablesInitialised
		{
			add => _addressablesInitialised += value;
			remove => _addressablesInitialised -= value;
		}

		public event UnityAction<float> DownloadPercentageUpdated
		{
			add => _downloadPercentageUpdated += value;
			remove => _downloadPercentageUpdated -= value;
		}

		public void RaiseAddressablesInitialisedEvent()
		{
			if (_addressablesInitialised == null){
				LoggerService.LogWarning($"{nameof(AddressablesEvents)}::{nameof(RaiseAddressablesInitialisedEvent)} raised, but nothing picked it up");
				return;
			}
			_addressablesInitialised.Invoke();
		}

		public void RaiseDownloadPercentageUpdatedEvent(float percentage)
		{
			if (_downloadPercentageUpdated == null){
				LoggerService.LogWarning($"{nameof(AddressablesEvents)}::{nameof(RaiseDownloadPercentageUpdatedEvent)} raised, but nothing picked it up");
				return;
			}
			_downloadPercentageUpdated.Invoke(percentage);
		}
	}
}