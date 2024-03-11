using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ClientConfig))]
	public class ClientConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/Client/Development Testing")]
		public static void ConfigForClientDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			ConfigForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Config/Client/Development Release")]
		public static void ConfigForClientDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			ConfigForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/Client/Production Testing")]
		public static void ConfigForClientProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				ConfigForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}
		
		[MenuItem("PeanutDashboard/Config/Client/Production Release")]
		public static void ConfigForClientProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				ConfigForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		private static void ConfigForClient(string addressableProfileId)
		{
			foreach (AddressableAssetGroup group in ProjectDatabase.Instance.addressableGroups){
				BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
				group.Settings.activeProfileId = addressableProfileId;
				var buildInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.BuildPath");
				var loadInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.LoadPath");
				schema.BuildPath.SetVariableById(group.Settings, buildInfo.Id);
				schema.LoadPath.SetVariableById(group.Settings, loadInfo.Id);
			}
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.WebGL, "");
		}
	}
}