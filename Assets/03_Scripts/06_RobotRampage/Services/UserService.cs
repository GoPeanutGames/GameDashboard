namespace PeanutDashboard._06_RobotRampage
{
    public static class UserService
    {
        private static string _userAddress;

        public static string UserAddress => _userAddress;

        public static void SetUserAddress(string address)
        {
            _userAddress = address;
        }
    }
}