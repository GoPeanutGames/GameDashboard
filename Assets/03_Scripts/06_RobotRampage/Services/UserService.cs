namespace PeanutDashboard._06_RobotRampage
{
    public static class UserService
    {
        private static bool _loggedOut;
        
        private static string _userAddress;

        private static string _tonToken;

        public static string UserAddress => _userAddress;

        public static bool LoggedOut => _loggedOut;

        public static string TonToken => _tonToken;

        public static void SetUserAddress(string address)
        {
            _userAddress = address;
        }

        public static void SetLoggedOut(bool loggedOut)
        {
            _loggedOut = loggedOut;
        }

        public static void SetTonToken(string tonToken)
        {
            _tonToken = tonToken;
        }
    }
}