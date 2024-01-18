using System;
using System.Runtime.InteropServices;
using AOT;
using MetaMask.Unity;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Device;

namespace PeanutDashboard.Shared.Authentication
{
    public class MetamaskService : Utils.Singleton<MetamaskService>
    {
        [DllImport("__Internal")]
        private static extern string GetURLFromPage();
        
        [DllImport("__Internal")]
        private static extern bool IsMobile();
        
        [DllImport("__Internal")]
        private static extern void Login(bool isDev, Action<string> cb);

        [DllImport("__Internal")]
        private static extern void RequestSignature(string schema, string address, Action<string> cb);

        public static void LoginMetamask()
        {
            LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(LoginMetamask)}");
            if (IsMobile())
            {
                // MetaMaskConfig metaMaskConfig = EnvironmentManager.Instance.GetMetamaskConfig();
                // string url = GetURLFromPage();
                
                MetaMaskUnity.Instance.Initialize();
                MetaMaskUnity.Instance.Connect();
                //TODO: on connect, try signature and then login
                //TODO: correct image on metamask (probably base64)
            }
            else
            {
                Login(EnvironmentManager.Instance.IsDev() ,OnMetamaskLoginSuccess);
            }
        }

        public static void RequestMetamaskSignature(string schema, string address)
        {
            LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(RequestMetamaskSignature)}");
            RequestSignature(schema, address, OnRequestSignatureSuccess);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnMetamaskLoginSuccess(string address)
        {
            LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnMetamaskLoginSuccess)} - {address}");
            AuthenticationEvents.Instance.userMetamaskConnected.Invoke(address);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnRequestSignatureSuccess(string signature)
        {
            LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(OnRequestSignatureSuccess)} - {signature}");
            AuthenticationEvents.Instance.userSignatureReceived.Invoke(signature);
        }
    }
}