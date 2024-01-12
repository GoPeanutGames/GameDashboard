using System;

namespace PeanutDashboard.Shared.User.Events
{
    public class UserEvents: Utils.Singleton<UserEvents>
    {
        public Action<bool> userLoggedIn;
    }
}