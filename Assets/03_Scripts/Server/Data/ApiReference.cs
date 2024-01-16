using System.Collections.Generic;

namespace PeanutDashboard.Server.Data
{
    public enum AuthenticationApi
    {
        GetLoginSchema,
        Web3LoginCheck
    }

    public enum PlayerApi
    {
        GetGeneralData,
        GetWallet
    }
    
    public static class ApiReference
    {
        public static readonly Dictionary<AuthenticationApi, string> AuthApiMap = new()
        {
            { AuthenticationApi.GetLoginSchema, "/auth/login-schema/" },
            { AuthenticationApi.Web3LoginCheck, "/auth/web3-login" },
        };

        public static readonly Dictionary<PlayerApi, string> PlayerApiMap = new()
        {
            { PlayerApi.GetGeneralData, "/player/me/" },
            { PlayerApi.GetWallet, "/player/wallet/" },
        };
    }
}