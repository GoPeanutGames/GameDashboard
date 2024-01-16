using System;

namespace PeanutDashboard.Server.Data
{
    [Serializable]
    public class CheckWeb3LoginRequest: BaseRequest
    {
        public string address;
    }

    [Serializable]
    public class CheckWeb3LoginResponse
    {
        public bool status;
    }
}