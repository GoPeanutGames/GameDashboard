using PeanutDashboard.Server;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User.Events;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared.User
{
	public class UserService : Singleton<UserService>
	{
		private User _currentUser;

		public void UserLogInComplete(string address, string signature)
		{
			LoggerService.LogInfo($"{nameof(UserService)}::{nameof(UserLogInComplete)}");
			_currentUser = new User()
			{
				loggedIn = true,
				walletAddress = address,
				signature = signature
			};
			UserEvents.Instance.RaiseUserLoggedInEvent(_currentUser.loggedIn);
			UserServerChannel.GetUserDataFromServer(_currentUser.walletAddress);
		}

		public void UserLoggedOut()
		{
			_currentUser = new User() { loggedIn = false };
			UserEvents.Instance.RaiseUserLoggedInEvent(_currentUser.loggedIn);
		}

		public void SetUserGeneralInfo(GeneralInfo generalInfo)
		{
			_currentUser.generalInfo = generalInfo;
		}

		public void SetUserWalletInfo(WalletInfo walletInfo)
		{
			_currentUser.walletInfo = walletInfo;
		}

		public bool IsLoggedIn()
		{
			return _currentUser is { loggedIn: true };
		}

		public string GetUserNickname()
		{
			return _currentUser?.generalInfo?.nickname ?? "";
		}

		public int GetUserBubbles()
		{
			return _currentUser?.walletInfo?.bubbles ?? 0;
		}

		public int GetUserGems()
		{
			return _currentUser?.walletInfo?.gems ?? 0;
		}

		public string GetUserAddress()
		{
			return _currentUser?.walletAddress ?? "";
		}
	}
}