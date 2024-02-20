using UnityEditor;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ServerConfig))]
	public class ServerConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/Server/Development Testing")]
		public static void ConfigForServerDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Config/Server/Development Release")]
		public static void ConfigForServerDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/Server/Production Testing")]
		public static void ConfigForServerProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}
		
		[MenuItem("PeanutDashboard/Config/Server/Production Release")]
		public static void ConfigForServerProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		private static void ConfigForServer(string addressableProfileId)
		{
			foreach (GameSceneConfig gameSceneConfig in ProjectDatabase.Instance.serverGameSceneConfigs){
				BundledAssetGroupSchema schema = gameSceneConfig.group.GetSchema<BundledAssetGroupSchema>();
				gameSceneConfig.group.Settings.activeProfileId = addressableProfileId;
				var buildInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.BuildPath");
				var loadInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.LoadPath");
				schema.BuildPath.SetVariableById(gameSceneConfig.group.Settings, buildInfo.Id);
				schema.LoadPath.SetVariableById(gameSceneConfig.group.Settings, loadInfo.Id);
			}
			EditorUserBuildSettings.SwitchActiveBuildTarget(NamedBuildTarget.Server, BuildTarget.StandaloneLinux64);
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Server, "SERVER");
		}
	}
}