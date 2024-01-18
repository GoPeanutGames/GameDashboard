using System;
using System.Runtime.InteropServices;
using AOT;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared.Authentication
{
    public class MetamaskService : Singleton<MetamaskService>
    {
        [DllImport("__Internal")]
        private static extern string Login(bool isDev, Action<string> cb);

        [DllImport("__Internal")]
        private static extern string RequestSignature(string schema, string address, Action<string> cb);

        public static void LoginMetamask()
        {
            LoggerService.LogInfo($"{nameof(MetamaskService)}::{nameof(LoginMetamask)}");
            Login(EnvironmentManager.Instance.IsDev() ,OnMetamaskLoginSuccess);
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