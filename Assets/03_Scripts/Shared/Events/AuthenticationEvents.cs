using System;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared.Events
{
    public class AuthenticationEvents: Singleton<AuthenticationEvents>
    {
        public Action<string> userMetamaskConnected;
        public Action<string> userSignatureReceived;
    }
}