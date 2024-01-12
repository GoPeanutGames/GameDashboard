using PeanutDashboard.Shared.User;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Authentication
{
    public class AuthenticationService : Singleton<AuthenticationService>
    {
        public void StartMetamaskLogin()
        {
            Debug.Log($"{nameof(AuthenticationService)}::{nameof(StartMetamaskLogin)}");
            //TODO: metamask stuff
            //TODO: server get data
            UserService.Instance.GetUserFromServer();
        }
    }
}

