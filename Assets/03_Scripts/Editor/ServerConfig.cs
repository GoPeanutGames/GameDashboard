using UnityEditor;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ServerConfig))]
	public class ServerConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/Config Server Development")]
		public static void ConfigForServerDev()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDev();
			ConfigForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/Config Server Production")]
		public static void ConfigForServerProd()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProd();
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