using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AOT;
using MetaMask.Models;
using MetaMask.Unity;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Metamask.Model;

namespace PeanutDashboard.Shared.Metamask
{
	public static class MetamaskService
	{
		[DllImport("__Internal")]
		private static extern bool IsMobile();

		[DllImport("__Internal")]
		private static extern void Login(bool isDev, Action<string> cb);

		[DllImport("__Internal")]
		private static extern void RequestSignature(string schema, string address, Action<string> cb);

		private static string WalletAddress => MetaMaskUnity.Instance.Wallet.ConnectedAddress.ToLower();
		private static long ChainId => EnvironmentManager.Instance.IsDev() ? ChainDataReference.MumbaiChainId : ChainDataReference.PolygonChainId;
		private static Chain ChainData => EnvironmentManager.Instance.IsDev() ? ChainDataReference.MumbaiChain : ChainDataReference.PolygonChain;

		public static void LoginMetamask()
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(LoginMetamask)}");
			if (IsMobile()){
				MetaMaskUnity.Instance.Initialize();
				MetaMaskUnity.Instance.Wallet.WalletConnectedHandler += MetamaskMobileConnected;
				MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler += MetamaskMobileAuthorized;
				MetaMaskUnity.Instance.Connect();
			}
			else{
				Login(EnvironmentManager.Instance.IsDev(), OnMetamaskLoginSuccess);
			}
		}

		public static void LogOutMetamask()
		{
			if (IsMobile()){
				MetaMaskUnity.Instance.Wallet.WalletConnectedHandler = null;
				MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler = null;
				MetaMaskUnity.Instance.Wallet.ChainIdChangedHandler = null;
				MetaMaskUnity.Instance.Wallet.AccountChangedHandler = null;
				if (MetaMaskUnity.Instance.Wallet.IsConnected){
					MetaMaskUnity.Instance.Wallet.Disconnect();
				}
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
			if (MetaMaskUnity.Instance.Wallet.ChainId != ChainId){
				MetaMaskUnity.Instance.Wallet.ChainIdChangedHandler += OnChainSwitched;
				MetaMaskUnity.Instance.Wallet.AccountChangedHandler += OnAccountChangeHandler;
				SwitchChain(ChainData);
			}
			else{
				AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(WalletAddress);
			}
		}

		private static void OnChainSwitched(object sender, EventArgs e)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnChainSwitched)}");
			if (MetaMaskUnity.Instance.Wallet.ChainId == ChainId){
				AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(WalletAddress);
				MetaMaskUnity.Instance.Wallet.ChainIdChangedHandler -= OnChainSwitched;
			}
		}

		private static void OnAccountChangeHandler(object sender, EventArgs e)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnAccountChangeHandler)}");
			if (MetaMaskUnity.Instance.Wallet.ChainId == ChainId){
				AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(WalletAddress);
				MetaMaskUnity.Instance.Wallet.AccountChangedHandler -= OnAccountChangeHandler;
			}
		}

		private static async Task<object> SwitchChain(Chain chain)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(SwitchChain)}");
			MetaMaskEthereumRequest signatureRequest = new()
			{
				Method = "wallet_addEthereumChain",
				Parameters = new[] { chain }
			};
			return MetaMaskUnity.Instance.Wallet.Request(signatureRequest);
		}

		private static async void RequestMobileSignature(string schema, string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(RequestMobileSignature)}");
			MetaMaskEthereumRequest signatureRequest = new()
			{
				Method = "eth_signTypedData_v4",
				Parameters = new[] { address, schema }
			};
			object result = await MetaMaskUnity.Instance.Wallet.Request(signatureRequest);
			AuthenticationEvents.Instance.RaiseUserSignatureReceivedEvent(result as string);
		}

		public static void RequestMetamaskSignature(string schema, string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(RequestMetamaskSignature)}");
			if (IsMobile()){
				RequestMobileSignature(schema, address);
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