using MetaMask.Unity;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Shared.Config
{
	[CreateAssetMenu(
		fileName = nameof(GameConfig) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.Configs + nameof(GameConfig)
	)]
	public class GameConfig: ScriptableObject
	{
		public EnvironmentModel currentEnvironmentModel;
		public MetaMaskConfig currentMetaMaskConfig;

		public MetaMaskConfig devMetamaskConfig;
		public MetaMaskConfig prodMetamaskConfig;
		public EnvironmentModel devEnvironmentModel;
		public EnvironmentModel prodEnvironmentModel;

		public void ConfigureForDev()
		{
			currentEnvironmentModel = devEnvironmentModel;
			currentMetaMaskConfig = devMetamaskConfig;
		}

		public void ConfigureForProd()
		{
			currentEnvironmentModel = prodEnvironmentModel;
			currentMetaMaskConfig = prodMetamaskConfig;
		}
	}
}