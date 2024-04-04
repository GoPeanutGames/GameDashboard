using UnityEditor;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(RPSServerConfig))]
	public class RPSServerConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Server/Development Testing")]
		public static void ConfigForServerDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Server/Development Release")]
		public static void ConfigForServerDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Server/Production Testing")]
		public static void ConfigForServerProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}
		
		[MenuItem("PeanutDashboard/Config/RockPaperScissors/Server/Production Release")]
		public static void ConfigForServerProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		private static void ConfigForServer(string addressableProfileId)
		{
			BundledAssetGroupSchema schema = ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.GetSchema<BundledAssetGroupSchema>();
			ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.Settings.activeProfileId = addressableProfileId;
			var buildInfo = ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.BuildPath");
			var loadInfo = ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.LoadPath");
			schema.BuildPath.SetVariableById(ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.Settings, buildInfo.Id);
			schema.LoadPath.SetVariableById(ProjectDatabase.Instance.rockPaperScissorsServerSceneConfig.group.Settings, loadInfo.Id);
			EditorUserBuildSettings.SwitchActiveBuildTarget(NamedBuildTarget.Server, BuildTarget.StandaloneLinux64);
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Server, "SERVER");
		}
	}
}