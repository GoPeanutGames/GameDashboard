using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;
using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using AOT;

namespace PeanutDashboard.Shared.Metamask
{
	public static class AuthenticationService
	{
		private static string _walletAddress;
		private static string _signature;
		private static string _env;

        
        public static void Initialise(string unityEnv)
		{

			_env = unityEnv;
			return;
			//MetamaskService.Initialise();
		}
		
		public static void StartMetamaskLogin()
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(StartMetamaskLogin)}");
			LoadingEvents.RaiseShowLoadingEvent("Connecting to metamask...");
#if !UNITY_EDITOR

#elif UNITY_EDITOR
			_signature = "0x821ee840b49c4294850eb51319b9ddb85504190ee38f4dec00f81b13b64fbd6a388d75df615de9aaac22adbc6b565134eaefa25e3b09223313932323e48c4aba1b";
			_walletAddress = "0x5d7167477bf3abedb261b4a5a1c150b87e6837a9";
			CheckWeb3Login();
#endif
		}
		
		public static void DisconnectMetamask()
		{
			UserService.Instance.UserLoggedOut();
		}

		private static void OnUserMetamaskConnected(string walletAddress)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnUserMetamaskConnected)}");
			LoadingEvents.RaiseUpdateLoadingEvent("Connecting to server...");
			_walletAddress = walletAddress;
			AuthenticationEvents.Instance.UserMetamaskConnected -= OnUserMetamaskConnected;
			AuthenticationEvents.Instance.MetamaskConnectionFail -= OnMetamaskConnectionFail;
			ServerService.GetDataFromServer(AuthenticationApi.GetLoginSchema, OnGetSchemaFromServer, walletAddress);
		}

		private static void OnMetamaskConnectionFail(string error)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskConnectionFail)} - reason: {error}");
			LoadingEvents.RaiseHideLoadingEvent();
			AuthenticationEvents.Instance.UserMetamaskConnected -= OnUserMetamaskConnected;
			AuthenticationEvents.Instance.MetamaskConnectionFail -= OnMetamaskConnectionFail;
		}

		private static void OnGetSchemaFromServer(string schema)
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnGetSchemaFromServer)}");
			AuthenticationEvents.Instance.UserSignatureReceived += OnMetamaskSignatureReceived;
			AuthenticationEvents.Instance.MetamaskSignatureFail += OnMetamaskSignatureFail;
			LoadingEvents.RaiseUpdateLoadingEvent("Requesting signature...");
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
			LoadingEvents.RaiseHideLoadingEvent();
			AuthenticationEvents.Instance.UserSignatureReceived -= OnMetamaskSignatureReceived;
			AuthenticationEvents.Instance.MetamaskSignatureFail -= OnMetamaskSignatureFail;
		}

		private static void CheckWeb3Login()
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3Login)}");
			LoadingEvents.RaiseUpdateLoadingEvent("Verifying...");
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
				SignInToUnity();
				UserService.Instance.UserLogInComplete(_walletAddress, _signature);
			}
			_walletAddress = "";
			_signature = "";
			LoadingEvents.RaiseHideLoadingEvent();
		}
		
		private static async Task SignInToUnity()
		{
			LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(SignInToUnity)}");
			InitializationOptions options = new ();
			options.SetEnvironmentName(_env);
			UnityServices.ExternalUserId = _walletAddress;
			await UnityServices.InitializeAsync(options);
			await Unity.Services.Authentication.AuthenticationService.Instance.SignInAnonymouslyAsync();
			LoggerService.LogInfo($"Signed in Anonymously as {Unity.Services.Authentication.AuthenticationService.Instance.PlayerId}");
		}
	}
}