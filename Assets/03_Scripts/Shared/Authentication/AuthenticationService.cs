using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User;
using UnityEngine;

namespace PeanutDashboard.Shared.Metamask
{
	public static class AuthenticationService
	{
		private static string _walletAddress;
		private static string _signature;

		public static void Initialise()
		{
			MetamaskService.Initialise();
		}
		
		public static void StartMetamaskLogin()
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(StartMetamaskLogin)}");
			LoadingEvents.Instance.RaiseShowLoadingEvent("Connecting to metamask...");
#if !UNITY_EDITOR
			AuthenticationEvents.Instance.UserMetamaskConnected += OnUserMetamaskConnected;
			AuthenticationEvents.Instance.MetamaskConnectionFail += OnMetamaskConnectionFail;
			MetamaskService.LoginMetamask();
#elif UNITY_EDITOR
			_signature = "0x821ee840b49c4294850eb51319b9ddb85504190ee38f4dec00f81b13b64fbd6a388d75df615de9aaac22adbc6b565134eaefa25e3b09223313932323e48c4aba1b";
			_walletAddress = "0x5d7167477bf3abedb261b4a5a1c150b87e6837a9";
			CheckWeb3Login();
#endif
		}
		
		public static void DisconnectMetamask()
		{
			UserService.Instance.UserLoggedOut();
			MetamaskService.LogOutMetamask();
		}

		private static void OnUserMetamaskConnected(string walletAddress)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnUserMetamaskConnected)}");
			LoadingEvents.Instance.RaiseUpdateLoadingEvent("Connecting to server...");
			_walletAddress = walletAddress;
			AuthenticationEvents.Instance.UserMetamaskConnected -= OnUserMetamaskConnected;
			AuthenticationEvents.Instance.MetamaskConnectionFail -= OnMetamaskConnectionFail;
			ServerService.GetDataFromServer(AuthenticationApi.GetLoginSchema, OnGetSchemaFromServer, walletAddress);
		}

		private static void OnMetamaskConnectionFail(string error)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskConnectionFail)} - reason: {error}");
			LoadingEvents.Instance.RaiseHideLoadingEvent();
			AuthenticationEvents.Instance.UserMetamaskConnected -= OnUserMetamaskConnected;
			AuthenticationEvents.Instance.MetamaskConnectionFail -= OnMetamaskConnectionFail;
		}

		private static void OnGetSchemaFromServer(string schema)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnGetSchemaFromServer)}");
			AuthenticationEvents.Instance.UserSignatureReceived += OnMetamaskSignatureReceived;
			AuthenticationEvents.Instance.MetamaskSignatureFail += OnMetamaskSignatureFail;
			LoadingEvents.Instance.RaiseUpdateLoadingEvent("Requesting signature...");
			MetamaskService.RequestMetamaskSignature(schema, _walletAddress);
		}

		private static void OnMetamaskSignatureReceived(string signature)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskSignatureReceived)}");
			_signature = signature;
			AuthenticationEvents.Instance.UserSignatureReceived -= OnMetamaskSignatureReceived;
			AuthenticationEvents.Instance.MetamaskSignatureFail -= OnMetamaskSignatureFail;
			CheckWeb3Login();
		}
		
		private static void OnMetamaskSignatureFail(string error)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskSignatureFail)} - reason: {error}");
			LoadingEvents.Instance.RaiseHideLoadingEvent();
			AuthenticationEvents.Instance.UserSignatureReceived -= OnMetamaskSignatureReceived;
			AuthenticationEvents.Instance.MetamaskSignatureFail -= OnMetamaskSignatureFail;
		}

		private static void CheckWeb3Login()
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3Login)}");
			LoadingEvents.Instance.RaiseUpdateLoadingEvent("Verifying...");
			CheckWeb3LoginRequest request = new CheckWeb3LoginRequest()
			{
				address = _walletAddress,
				signature = _signature
			};
			string requestJson = JsonUtility.ToJson(request);
			ServerService.PostDataToServer(AuthenticationApi.Web3LoginCheck, requestJson, CheckWeb3LoginCallback);
		}

		private static void CheckWeb3LoginCallback(string result)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3LoginCallback)}");
			CheckWeb3LoginResponse response = JsonUtility.FromJson<CheckWeb3LoginResponse>(result);
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3LoginCallback)} - Web3 Login check: {response.status}");
			if (response.status){
				UserService.Instance.UserLogInComplete(_walletAddress, _signature);
			}
			_walletAddress = "";
			_signature = "";
			LoadingEvents.Instance.RaiseHideLoadingEvent();
		}
	}
}