using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(RPSTelegramConfig))]
	public class RPSTelegramConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Telegram/Development Testing")]
		public static void ConfigForClientTelegramRPSDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			ConfigForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Telegram/Development Release")]
		public static void ConfigForClientTelegramRPSDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			ConfigForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Telegram/Production Testing")]
		public static void ConfigForClientTelegramRPSProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for Telegram production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				ConfigForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Telegram/Production Release")]
		public static void ConfigForClientTelegramRPSProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for Telegram production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				ConfigForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		private static void ConfigForClientTelegram(string addressableProfileId)
		{
			GameSceneConfig rpsGameSceneConfig = ProjectDatabase.Instance.rockPaperScissorsSceneConfig;
			AddressableAssetGroup group = rpsGameSceneConfig.group;
			BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
			group.Settings.activeProfileId = addressableProfileId;
			var buildInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.BuildPath");
			var loadInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.LoadPath");
			schema.BuildPath.SetVariableById(group.Settings, buildInfo.Id);
			schema.LoadPath.SetVariableById(group.Settings, loadInfo.Id);
			
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.WebGL, "");
		}
	}
}