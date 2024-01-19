using System;
using System.Runtime.InteropServices;
using AOT;
using MetaMask.Models;
using MetaMask.Unity;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;

namespace PeanutDashboard.Shared.Authentication
{
	[Serializable]
	public class MetamaskSignatureData
	{
		public string id;
		public string jsonrpc;
		public string result;
	}

	[Serializable]
	public class MetamaskSignatureResponse
	{
		public string name;
		public MetamaskSignatureData data;
	}

	public class MetamaskService : Utils.Singleton<MetamaskService>
	{
		[DllImport("__Internal")]
		private static extern bool IsMobile();

		[DllImport("__Internal")]
		private static extern void Login(bool isDev, Action<string> cb);

		[DllImport("__Internal")]
		private static extern void RequestSignature(string schema, string address, Action<string> cb);

		public static string WalletAddress => MetaMaskUnity.Instance.Wallet.ConnectedAddress.ToLower();
		
		public static void LoginMetamask()
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(LoginMetamask)}");
			if (IsMobile()){
				MetaMaskUnity.Instance.Initialize();
				MetaMaskUnity.Instance.Wallet.WalletConnectedHandler += MetamaskMobileConnected;
				MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler += MetamaskMobileAuthorized;
				MetaMaskUnity.Instance.Connect();
				//TODO: request chain switch if needed
			}
			else{
				Login(EnvironmentManager.Instance.IsDev(), OnMetamaskLoginSuccess);
			}
		}

		private static void MetamaskMobileConnected(object sender, EventArgs e)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(MetamaskMobileConnected)} - {e}");
			MetaMaskUnity.Instance.Wallet.WalletConnectedHandler -= MetamaskMobileConnected;
		}

		private static void MetamaskMobileAuthorized(object sender, EventArgs e)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(MetamaskMobileAuthorized)} - {e}");
			MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler -= MetamaskMobileAuthorized;
			AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(WalletAddress);
		}

		public static async void RequestMetamaskSignature(string schema, string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(RequestMetamaskSignature)}");
			if (IsMobile()){
				MetaMaskEthereumRequest signatureRequest = new()
				{
					Method = "eth_signTypedData_v4",
					Parameters = new[] { address, schema }
				};
				object result = await MetaMaskUnity.Instance.Wallet.Request(signatureRequest);
				AuthenticationEvents.Instance.RaiseUserSignatureReceivedEvent(result as string);
			}
			else{
				RequestSignature(schema, address, OnRequestSignatureSuccess);
			}
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnMetamaskLoginSuccess(string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnMetamaskLoginSuccess)} - {address}");
			AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(address);
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnRequestSignatureSuccess(string signature)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnRequestSignatureSuccess)} - {signature}");
			AuthenticationEvents.Instance.RaiseUserSignatureReceivedEvent(signature);
		}
	}
}