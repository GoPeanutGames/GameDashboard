using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ClientBuilder))]
	public class ClientBuilder: UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Build Client Development")]
		public static void BuildForClientDev()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDev();
			BuildForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Build Client Production")]
		public static void BuildForClientProd()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForProd();
			BuildForClient(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		private static void BuildForClient(string addressableProfileId)
		{
			// Get main folder path.
			string parentFolderPath = EditorUtility.SaveFolderPanel("Choose the main folder", "", "");
			
			if (string.IsNullOrWhiteSpace(parentFolderPath)) {
				Debug.Log(
					$"{nameof(ServerBuilder)}::{nameof(BuildForClient)}:: parent folder not selected, returning!");
			
				return;
			}
			
			foreach (AddressableAssetGroup group in ProjectDatabase.Instance.addressableGroups){
				BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
				group.Settings.activeProfileId = addressableProfileId;
				var buildInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.BuildPath");
				var loadInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.LoadPath");
				schema.BuildPath.SetVariableById(group.Settings, buildInfo.Id);
				schema.LoadPath.SetVariableById(group.Settings, loadInfo.Id);
			}
			AddressableAssetSettings.BuildPlayerContent();
			string folderName = "Dashboard";
			BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
			{
				scenes = new []{"Assets/01_Scenes/Core/InitScene.unity"},
				locationPathName = Path.Combine(parentFolderPath, folderName),
				target = BuildTarget.WebGL,
			};
			BuildPipeline.BuildPlayer(buildPlayerOptions);
		}
	}
}