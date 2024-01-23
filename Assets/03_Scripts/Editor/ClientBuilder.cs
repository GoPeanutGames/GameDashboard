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
		[MenuItem("PeanutDashboard/Build Client")]
		public static void BuildForClient()
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