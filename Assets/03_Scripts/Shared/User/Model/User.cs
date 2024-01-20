namespace PeanutDashboard.Shared.User
{
	public class User
	{
		public bool loggedIn;
		public string walletAddress;
		public string signature;
		public GeneralInfo generalInfo;
		public WalletInfo walletInfo;
	}

	public class GeneralInfo
	{
		public string nickname;
	}

	public class WalletInfo
	{
		public int gems;
		public int bubbles;
	}
}