using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
    public static class UserService
    {
        private static bool _loggedOut;
        
        private static string _userAddress;

        private static string _tonToken;

        private static int _gemsAmount;

        private static int _nutzAmount;

        private static int _pointsAmount;

        public static string UserAddress => _userAddress;

        public static bool LoggedOut => _loggedOut;

        public static string TonToken => _tonToken;

        public static int GemsAmount => _gemsAmount;

        public static int NutzAmount => _nutzAmount;

        public static int PointsAmount => _pointsAmount;

        public static event UnityAction pointsUpdated;
        
        public static event UnityAction gemsUpdated;
        
        public static event UnityAction nutzUpdated;

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

        public static void SetGems(int gems)
        {
            _gemsAmount = gems;
            gemsUpdated?.Invoke();
        }
        
        public static void SetNutz(int nutz)
        {
            _nutzAmount = nutz;
            nutzUpdated?.Invoke();
        }
        
        public static void SetPoints(int points)
        {
            _pointsAmount = points;
            pointsUpdated?.Invoke();
        }
    }
}