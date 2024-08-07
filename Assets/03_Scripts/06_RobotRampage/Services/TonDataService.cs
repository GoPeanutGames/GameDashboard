using PeanutDashboard.Server;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public static class TonDataService
	{
		public static void GetAccountData()
		{
			ServerService.GetDataFromServer(TonUserApi.GetUserData, GetAccountDataSuccess);
		}

		private static void GetAccountDataSuccess(string response)
		{
			GetUserData userData = JsonUtility.FromJson<GetUserData>(response);
			UserService.SetUserAddress(userData.player.address);
			UserService.SetGems(userData.player.wallet.gems);
			UserService.SetPoints(userData.player.wallet.bubbles);
			UserService.SetNutz(userData.player.wallet.fragments);
		}
	}
}