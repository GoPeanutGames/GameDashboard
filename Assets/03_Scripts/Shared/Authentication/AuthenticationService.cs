using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Authentication
{
    public class AuthenticationService : Singleton<AuthenticationService>
    {
        private string _walletAddress;
        private string _signature;
        
        public void StartMetamaskLogin()
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(StartMetamaskLogin)}");
            AuthenticationEvents.Instance.userMetamaskConnected += OnUserMetamaskConnected;
            MetamaskService.LoginMetamask();
        }

        private void OnUserMetamaskConnected(string walletAddress)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnUserMetamaskConnected)}");
            _walletAddress = walletAddress;
            AuthenticationEvents.Instance.userMetamaskConnected -= OnUserMetamaskConnected;
            ServerService.Instance.GetSchemaDataFromServer(AuthenticationApi.GetLoginSchema, OnGetSchemaFromServer, walletAddress);
        }

        private void OnGetSchemaFromServer(string schema)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnGetSchemaFromServer)}");
            AuthenticationEvents.Instance.userSignatureReceived += OnMetamaskSignatureReceived;
            MetamaskService.RequestMetamaskSignature(schema, _walletAddress);
        }

        private void OnMetamaskSignatureReceived(string signature)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(OnMetamaskSignatureReceived)}");
            _signature = signature;
            AuthenticationEvents.Instance.userSignatureReceived -= OnMetamaskSignatureReceived;
            CheckWeb3Login();
        }

        private void CheckWeb3Login()
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

        private void CheckWeb3LoginCallback(string result)
        {
            LoggerService.LogInfo($"{nameof(AuthenticationService)}::{nameof(CheckWeb3LoginCallback)}");
            CheckWeb3LoginResponse response = JsonUtility.FromJson<CheckWeb3LoginResponse>(result);
            if (response.status)
            {
                UserService.Instance.UserLogInComplete(_walletAddress, _signature);
            }
            _walletAddress = "";
            _signature = "";
        }
    }
}

