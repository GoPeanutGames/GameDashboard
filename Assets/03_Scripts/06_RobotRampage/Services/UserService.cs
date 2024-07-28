namespace PeanutDashboard._06_RobotRampage
{
    public static class UserService
    {
        private static bool _loggedOut;
        
        private static string _userAddress;

        public static string UserAddress => _userAddress;

        public static bool LoggedOut => _loggedOut;

        public static void SetUserAddress(string address)
        {
            _userAddress = address;
        }

        public static void SetLoggedOut(bool loggedOut)
        {
            _loggedOut = loggedOut;
        }
    }
}