using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User.Events;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.User
{
    public class UserService : Singleton<UserService>
    {
        private User _currentUser;

        public void UserLogInComplete(string address, string signature)
        {
            LoggerService.LogInfo($"{nameof(UserService)}::{nameof(UserLogInComplete)}");
            _currentUser = new User()
            {
                loggedIn = true,
                walletAddress = address,
                signature = signature
            };
            GetUserDataFromServer();
        }

        public void UserLoggedOut()
        {
            _currentUser = new User() { loggedIn = false };
            UserEvents.Instance.userLoggedIn.Invoke(_currentUser.loggedIn);
        }
        
        public void GetUserDataFromServer()
        {
            LoggerService.LogInfo($"{nameof(UserService)}::{nameof(GetUserDataFromServer)}");
            ServerService.Instance.GetPlayerDataFromServer(PlayerApi.GetGeneralData, GetGeneralDataSuccess, _currentUser.walletAddress);
        }

        private void GetGeneralDataSuccess(string response)
        {
            LoggerService.LogInfo($"{nameof(UserService)}::{nameof(GetGeneralDataSuccess)} - {response}");
            GetGeneralDataResponse getGeneralDataResponse = JsonUtility.FromJson<GetGeneralDataResponse>(response);
            _currentUser.generalInfo = new()
            {
                nickname = getGeneralDataResponse.nickname
            };
            ServerService.Instance.GetPlayerDataFromServer(PlayerApi.GetWallet, GetWalletDataSuccess, _currentUser.walletAddress);
        }

        private void GetWalletDataSuccess(string response)
        {
            LoggerService.LogInfo($"{nameof(UserService)}::{nameof(GetWalletDataSuccess)} - {response}");
            GetPlayerWalletResponse getGeneralDataResponse = JsonUtility.FromJson<GetPlayerWalletResponse>(response);
            _currentUser.walletInfo = new()
            {
                gems = getGeneralDataResponse.gems,
                bubbles = getGeneralDataResponse.bubbles
            };
            UserEvents.Instance.userLoggedIn.Invoke(_currentUser.loggedIn);
            UserEvents.Instance.userGeneralInfoUpdated?.Invoke();
            UserEvents.Instance.userResourcesUpdated?.Invoke();
        }

        public bool IsLoggedIn()
        {
            return _currentUser is { loggedIn: true };
        }

        public string GetUserNickname()
        {
            return _currentUser != null ? _currentUser.generalInfo.nickname : "";
        }

        public int GetUserBubbles()
        {
            return _currentUser != null ? _currentUser.walletInfo.bubbles : 0;
        }

        public int GetUserGems()
        {
            return _currentUser != null ? _currentUser.walletInfo.gems : 0;
        }
    }
}