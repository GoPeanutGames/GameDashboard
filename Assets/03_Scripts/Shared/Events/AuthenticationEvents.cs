using Org.BouncyCastle.Asn1.Cms;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine.Events;

namespace PeanutDashboard.Shared.Events
{
	public class AuthenticationEvents : Singleton<AuthenticationEvents>
	{
		private UnityAction<string> _userMetamaskConnected;
		private UnityAction<string> _userSignatureReceived;
		private UnityAction<string> _metamaskConnectionFail;
		private UnityAction<string> _metamaskSignatureFail;
		private UnityAction<AuthenticationData> _authenticationDataRetrievalSuccess;
		private UnityAction _authenticationDataRetrievalFail;

		public event UnityAction<AuthenticationData> AuthenticationDataRetrievalSuccess
		{
			add => _authenticationDataRetrievalSuccess += value;
			remove => _authenticationDataRetrievalSuccess -= value;
        }

        public event UnityAction AuthenticationDataRetrievalFail
        {
            add => _authenticationDataRetrievalFail += value;
            remove => _authenticationDataRetrievalFail -= value;
        }


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

		public event UnityAction<string> MetamaskConnectionFail
		{
			add => _metamaskConnectionFail += value;
			remove => _metamaskConnectionFail -= value;
		}
		
		public event UnityAction<string> MetamaskSignatureFail
		{
			add => _metamaskSignatureFail += value;
			remove => _metamaskSignatureFail -= value;
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
		
		public void RaiseMetamaskConnectionFailEvent(string error)
		{
			if (_metamaskConnectionFail == null){
				LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseMetamaskConnectionFailEvent)} raised, but nothing picked it up");
				return;
			}
			_metamaskConnectionFail.Invoke(error);
		}
		
		public void RaiseMetamaskSignatureFailEvent(string error)
		{
			if (_metamaskSignatureFail == null){
				LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseMetamaskSignatureFailEvent)} raised, but nothing picked it up");
				return;
			}
			_metamaskSignatureFail.Invoke(error);
		}

		public void RaiseAuthenticationDataRetrievalSuccessEvent(AuthenticationData authenticationData)
		{
			if (_authenticationDataRetrievalSuccess == null)
			{
                LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseAuthenticationDataRetrievalSuccessEvent)} raised, but nothing picked it up");
                return;
            }
			_authenticationDataRetrievalSuccess.Invoke(authenticationData);
		}

		public void RaiseAuthenticationDataRetrievalFailEvent()
		{
			if (_authenticationDataRetrievalFail == null)
			{
                LoggerService.LogWarning($"{nameof(AuthenticationEvents)}::{nameof(RaiseAuthenticationDataRetrievalFailEvent)} raised, but nothing picked it up");
				return;
            }
			_authenticationDataRetrievalFail.Invoke();
		}

    }
}