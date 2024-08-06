using System;
using System.Collections.Generic;

namespace PeanutDashboard._06_RobotRampage
{
	[Serializable]
	public class GetUserData
	{
		public string message;
		public AccountInfo accountInfo;
		public PlayerData player;
	}

	[Serializable]
	public class AccountInfo
	{
		public string address;
		public int balance;
	}

	[Serializable]
	public class PlayerData
	{
		public string address;
		public WalletData wallet;
	}

	[Serializable]
	public class WalletData
	{
		public int gems;
		public int energy;
		public int bubbles;
		public int fragments;
		public List<string> gameItems;
		public string fullAddress;
	}
}