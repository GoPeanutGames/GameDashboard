using PeanutDashboard.Shared.User.Events;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.User
{
    public class UserService : Singleton<UserService>
    {
        private User _currentUser;

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public void GetUserFromServer()
        {
            Debug.Log($"{nameof(UserService)}::{nameof(GetUserFromServer)}");
            //TODO: get from server depending on metamask/auth/something
            //TODO: create user from that data (not here)
            //TODO: depending on log in status fals/true
            _currentUser = new User() { loggedIn = true };
            UserEvents.Instance.userLoggedIn.Invoke(_currentUser.loggedIn);
        }

        public bool IsLoggedIn()
        {
            return _currentUser is { loggedIn: true };
        }
    }
}