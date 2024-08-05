using PeanutDashboard.Server;
using PeanutDashboard.Shared.Logging;

namespace PeanutDashboard._06_RobotRampage
{
	public static class TonDataService
	{
		public static void GetAccountData()
		{
			ServerService.GetDataFromServer(TonUserApi.GetUserData, (data)=> LoggerService.LogWarning(data));
		}
	}
}