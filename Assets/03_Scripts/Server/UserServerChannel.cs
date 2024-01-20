using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;

namespace PeanutDashboard.Server
{
	public static class UserServerChannel
	{
		public static void GetUserDataFromServer(string walletAddress)
		{
			LoggerService.LogInfo($"{nameof(UserServerChannel)}::{nameof(GetUserDataFromServer)}");
			ServerService.GetDataFromServer<PlayerApi, GetGeneralDataResponse, string>(PlayerApi.GetGeneralData, GetGeneralDataSuccess, walletAddress);
		}

		private static void GetGeneralDataSuccess(GetGeneralDataResponse getGeneralDataResponse)
		{
			LoggerService.LogInfo($"{nameof(UserServerChannel)}::{nameof(GetGeneralDataSuccess)} - nickname: {getGeneralDataResponse.nickname}");
			GeneralInfo generalInfo = new()
			{
				nickname = getGeneralDataResponse.nickname
			};
			UserService.Instance.SetUserGeneralInfo(generalInfo);
			UserEvents.Instance.RaiseUserGeneralInfoUpdatedEvent();
			ServerService.GetDataFromServer<PlayerApi, GetPlayerWalletResponse, string>(PlayerApi.GetWallet, GetWalletDataSuccess, UserService.Instance.GetUserAddress());
		}

		private static void GetWalletDataSuccess(GetPlayerWalletResponse getPlayerWalletResponse)
		{
			LoggerService.LogInfo($"{nameof(UserServerChannel)}::{nameof(GetWalletDataSuccess)} - gems: {getPlayerWalletResponse.gems}, bubbles: {getPlayerWalletResponse.bubbles}");
			WalletInfo walletInfo = new()
			{
				gems = getPlayerWalletResponse.gems,
				bubbles = getPlayerWalletResponse.bubbles
			};
			UserService.Instance.SetUserWalletInfo(walletInfo);
			UserEvents.Instance.RaiseUserResourcesUpdatedEvent();
		}
	}
}