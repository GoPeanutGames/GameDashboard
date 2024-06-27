using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Shared.Config
{
	[CreateAssetMenu(
		fileName = nameof(GameConfig) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardConfigs + nameof(GameConfig)
	)]
	public class GameConfig: ScriptableObject
	{
		public EnvironmentModel currentEnvironmentModel;
		public EnvironmentModel devTestingEnvironmentModel;
		public EnvironmentModel devReleaseEnvironmentModel;
		public EnvironmentModel prodTestingEnvironmentModel;
		public EnvironmentModel prodReleaseEnvironmentModel;

		public void ConfigureForDevTesting()
		{
			currentEnvironmentModel = devTestingEnvironmentModel;
		}
		
		public void ConfigureForDevRelease()
		{
			currentEnvironmentModel = devReleaseEnvironmentModel;
		}

		public void ConfigureForProdTesting()
		{
			currentEnvironmentModel = prodTestingEnvironmentModel;
		}

		public void ConfigureForProdRelease()
		{
			currentEnvironmentModel = prodReleaseEnvironmentModel;
		}
	}
}