using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public class AuthenticationEvents : Singleton<AuthenticationEvents>
	{
		private UnityAction<string> _userMetamaskConnected;
		private UnityAction<string> _userSignatureReceived;

		public event UnityAction<string> UserMetamaskConnected
		{
			add => _userMetamaskConnected += value;
			remove => _userMetamaskConnected -= value;
		}

		public event UnityAction<string> UserSignatureReceived
		{
			add => _userSignatureReceived += value;
			remove => _userSignatureReceived -= value;
		}

		public void RaiseUserMetamaskConnectedEvent(string address)
		{
			if (_userMetamaskConnected == null){
				LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseUserMetamaskConnectedEvent)} raised, but nothing picked it up");
				return;
			}
			_userMetamaskConnected.Invoke(address);
		}

		public void RaiseUserSignatureReceivedEvent(string address)
		{
			if (_userSignatureReceived == null){
				LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseUserSignatureReceivedEvent)} raised, but nothing picked it up");
				return;
			}
			_userSignatureReceived.Invoke(address);
		}
	}
}