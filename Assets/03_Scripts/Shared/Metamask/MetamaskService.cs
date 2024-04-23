using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AOT;
using MetaMask;
using MetaMask.Models;
using MetaMask.Unity;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Metamask.Model;
using PeanutDashboard.Utils.WebGL;
using UnityEngine;

namespace PeanutDashboard.Shared.Metamask
{
	public static class MetamaskService
	{

		[DllImport("__Internal")]
		private static extern void Login(bool isDev, Action<string> cbSuccess, Action<string> cbFail);

		[DllImport("__Internal")]
		private static extern void RequestSignature(string schema, string address, Action<string> cbSuccess, Action<string> cbFail);





		private static string WalletAddress => MetaMaskUnity.Instance.Wallet.ConnectedAddress.ToLower();
		private static long ChainId => EnvironmentManager.Instance.IsDev() ? ChainDataReference.SepoliaChainId : ChainDataReference.BlastChainId;
		private static Chain ChainData => EnvironmentManager.Instance.IsDev() ? ChainDataReference.MumbaiChain : ChainDataReference.PolygonChain;

		private static bool _metamaskInitialised = false;

		public static void Initialise(MetaMaskConfig metaMaskConfig)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(Initialise)}");
			if (!_metamaskInitialised){
				MetaMaskUnity.Instance.Initialize(metaMaskConfig);
				_metamaskInitialised = true;
			}
		}
		
		public static void LoginMetamask()
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(LoginMetamask)}");
			if (WebGLUtils.IsWebMobile){
				LoadingEvents.RaiseUpdateLoadingEvent("Connecting to metamask app...");
				MetaMaskUnity.Instance.Wallet.EthereumRequestFailedHandler += MobileConnectMetamaskFailHandler;
				MetaMaskUnity.Instance.Wallet.WalletConnectedHandler += MetamaskMobileConnected;
				MetaMaskUnity.Instance.Wallet.WalletDisconnectedHandler += MobileWalletDisconnected;
				MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler += MetamaskMobileAuthorized;
				MetaMaskUnity.Instance.Connect();
			}
			else{
				Login(EnvironmentManager.Instance.IsDev(), OnMetamaskLoginSuccess, OnMetamaskLoginFail);
			}
		}

		private static void MobileWalletDisconnected(object sender, EventArgs e)
		{
			LoggerService.LogError($"{nameof(MetamaskService)}::{nameof(LoginMetamask)} - Mobile wallet disconnected");
			LogOutMetamask();
		}
		
		private static void MobileConnectMetamaskFailHandler(object sender, EventArgs e)
		{
			MetaMaskEthereumRequestFailedEventArgs failedEventArgs = e as MetaMaskEthereumRequestFailedEventArgs;
			LoggerService.LogError($"{nameof(MetamaskService)}::{nameof(LoginMetamask)} - Fail: {failedEventArgs.Error.Error.Message}");
			MetaMaskUnity.Instance.Wallet.EthereumRequestFailedHandler -= MobileConnectMetamaskFailHandler;
			LogOutMetamask();
			LoadingEvents.RaiseHideLoadingEvent();
		}
		
		public static void LogOutMetamask()
		{
			if (WebGLUtils.IsWebMobile){
				MetaMaskUnity.Instance.Wallet.WalletConnectedHandler = null;
				MetaMaskUnity.Instance.Wallet.WalletAuthorizedHandler = null;
				MetaMaskUnity.Instance.Wallet.ChainIdChangedHandler = null;
				MetaMaskUnity.Instance.Wallet.AccountChangedHandler = null;
				MetaMaskUnity.Instance.Wallet.WalletDisconnectedHandler = null;
				MetaMaskUnity.Instance.Wallet.EthereumRequestFailedHandler = null;
				if (MetaMaskUnity.Instance.Wallet.IsConnected){
					MetaMaskUnity.Instance.Disconnect();
				}
				MetaMaskUnity.Instance.Wallet.EndSession(true);
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
				LoadingEvents.RaiseUpdateLoadingEvent("Switching chain...");
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
			LoadingEvents.RaiseUpdateLoadingEvent("Requesting signature...");
			MetaMaskEthereumRequest signatureRequest = new()
			{
				Method = "eth_signTypedData_v4",
				Parameters = new[] { address, schema }
			};
			object result = await MetaMaskUnity.Instance.Wallet.Request(signatureRequest);
			MetaMaskUnity.Instance.Wallet.EthereumRequestFailedHandler -= MobileConnectMetamaskFailHandler;
			AuthenticationEvents.Instance.RaiseUserSignatureReceivedEvent(result as string);
		}

		public static void RequestMetamaskSignature(string schema, string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(RequestMetamaskSignature)}");
			if (WebGLUtils.IsWebMobile)
			{
				RequestMobileSignature(schema, address);
			}
			else
			{
				RequestSignature(schema, address, OnRequestSignatureSuccess, OnRequestSignatureFail);
			}
		}





        [MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnMetamaskLoginSuccess(string address)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnMetamaskLoginSuccess)} - {address}");
			AuthenticationEvents.Instance.RaiseUserMetamaskConnectedEvent(address);
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnMetamaskLoginFail(string error)
		{
			LoggerService.LogError($"{nameof(MetamaskService)}::{nameof(OnMetamaskLoginFail)} - {error}");
			AuthenticationEvents.Instance.RaiseMetamaskConnectionFailEvent(error);
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnRequestSignatureSuccess(string signature)
		{
			LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnRequestSignatureSuccess)} - {signature}");
			AuthenticationEvents.Instance.RaiseUserSignatureReceivedEvent(signature);
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void OnRequestSignatureFail(string error)
		{
			LoggerService.LogError($"{nameof(MetamaskService)}::{nameof(OnRequestSignatureFail)} - {error}");
			AuthenticationEvents.Instance.RaiseMetamaskSignatureFailEvent(error);
		}
	}
}