using PeanutDashboard.Init;

namespace PeanutDashboard.Shared.Picker
{
	public static class GameNetworkSyncService
	{
		private static GameInfo _currentGameInfo;
		
		public static void AssignCurrentGameInfo(GameInfo currentGameInfo)
		{
			_currentGameInfo = currentGameInfo;
		}

		public static SceneInfo GetCurrentGameNetworkScene()
		{
			return _currentGameInfo.networkStartScene;
		}

		public static SceneInfo GetNetworkEntryPoint()
		{
			return _currentGameInfo.networkEntryPointScene;
		}
	}
}