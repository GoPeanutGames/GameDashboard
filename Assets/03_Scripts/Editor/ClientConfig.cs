using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ClientConfig))]
	public class ClientConfig : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Config/Config Client Development")]
		public static void ConfigForClientDev()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDev();
			ConfigForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Config/Config Client Production")]
		public static void ConfigForClientProd()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProd();
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