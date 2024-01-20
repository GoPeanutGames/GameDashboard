using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Metamask
{
    public static class AuthenticationService
    {
        private static string _walletAddress;
        private static string _signature;
        
        public static void StartMetamaskLogin()
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(StartMetamaskLogin)}");
            AuthenticationEvents.Instance.UserMetamaskConnected += OnUserMetamaskConnected;
            MetamaskService.LoginMetamask();
        }

        public static void DisconnectMetamask()
        {
            UserService.Instance.UserLoggedOut();
            MetamaskService.LogOutMetamask();
        }

        private static void OnUserMetamaskConnected(string walletAddress)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnUserMetamaskConnected)}");
            _walletAddress = walletAddress;
            AuthenticationEvents.Instance.UserMetamaskConnected -= OnUserMetamaskConnected;
            ServerService.Instance.GetSchemaDataFromServer(AuthenticationApi.GetLoginSchema, OnGetSchemaFromServer, walletAddress);
        }

        private static void OnGetSchemaFromServer(string schema)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnGetSchemaFromServer)}");
            AuthenticationEvents.Instance.UserSignatureReceived += OnMetamaskSignatureReceived;
            MetamaskService.RequestMetamaskSignature(schema, _walletAddress);
        }

        private static void OnMetamaskSignatureReceived(string signature)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskSignatureReceived)}");
            _signature = signature;
            AuthenticationEvents.Instance.UserSignatureReceived -= OnMetamaskSignatureReceived;
            CheckWeb3Login();
        }

        private static void CheckWeb3Login()
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3Login)}");
            CheckWeb3LoginRequest request = new CheckWeb3LoginRequest()
            {
                address = _walletAddress,
                signature = _signature
            };
            string requestJson = JsonUtility.ToJson(request);
            ServerService.Instance.CheckWeb3Login(AuthenticationApi.Web3LoginCheck, requestJson, CheckWeb3LoginCallback);
        }

        private static void CheckWeb3LoginCallback(string result)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3LoginCallback)}");
            CheckWeb3LoginResponse response = JsonUtility.FromJson<CheckWeb3LoginResponse>(result);
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3LoginCallback)} - Web3 Login check: {response.status}");
            if (response.status)
            {
                UserService.Instance.UserLogInComplete(_walletAddress, _signature);
            }
            _walletAddress = "";
            _signature = "";
        }
    }
}

