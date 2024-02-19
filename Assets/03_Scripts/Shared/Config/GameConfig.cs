using MetaMask.Unity;
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
		public MetaMaskConfig currentMetaMaskConfig;

		public MetaMaskConfig devMetamaskConfig;
		public MetaMaskConfig prodMetamaskConfig;
		public EnvironmentModel devTestingEnvironmentModel;
		public EnvironmentModel devReleaseEnvironmentModel;
		public EnvironmentModel prodTestingEnvironmentModel;
		public EnvironmentModel prodReleaseEnvironmentModel;

		public void ConfigureForDevTesting()
		{
			currentEnvironmentModel = devTestingEnvironmentModel;
			currentMetaMaskConfig = devMetamaskConfig;
		}
		
		public void ConfigureForDevRelease()
		{
			currentEnvironmentModel = devReleaseEnvironmentModel;
			currentMetaMaskConfig = devMetamaskConfig;
		}

		public void ConfigureForProdTesting()
		{
			currentEnvironmentModel = prodTestingEnvironmentModel;
			currentMetaMaskConfig = prodMetamaskConfig;
		}

		public void ConfigureForProdRelease()
		{
			currentEnvironmentModel = prodReleaseEnvironmentModel;
			currentMetaMaskConfig = prodMetamaskConfig;
		}
	}
}