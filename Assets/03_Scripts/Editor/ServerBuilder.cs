using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ServerBuilder))]
	public class ServerBuilder: UnityEditor.Editor
	{
		private static readonly string ServerEntryPointScene = "EntryPoint";

		[MenuItem("PeanutDashboard/Build Server")]
		public static void BuildForServer()
		{
			// Get main folder path.
			string parentFolderPath = EditorUtility.SaveFolderPanel("Choose the main folder", "", "");
			
			if (string.IsNullOrWhiteSpace(parentFolderPath)) {
				Debug.Log(
					$"{nameof(ServerBuilder)}::{nameof(BuildForServer)}:: parent folder not selected, returning!");
			
				return;
			}

			List<string> scenesInBuild = new List<string>();
			scenesInBuild.Add($"Assets/01_Scenes/Server/{ServerEntryPointScene}.unity");
			foreach (GameSceneConfig gameSceneConfig in ProjectDatabase.Instance.serverGameSceneConfigs){
				scenesInBuild.Add(gameSceneConfig.scenePath);
				BundledAssetGroupSchema schema = gameSceneConfig.group.GetSchema<BundledAssetGroupSchema>();
				var buildInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.BuildPath");
				var loadInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.LoadPath");
				schema.BuildPath.SetVariableById(gameSceneConfig.group.Settings, buildInfo.Id);
				schema.LoadPath.SetVariableById(gameSceneConfig.group.Settings, loadInfo.Id);
			}
			string folderName = "Server";
			BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
			{
				scenes = scenesInBuild.ToArray(),
				locationPathName = Path.Combine(parentFolderPath, folderName),
				target = BuildTarget.StandaloneLinux64,
				subtarget = (int)StandaloneBuildSubtarget.Server
			};
			BuildPipeline.BuildPlayer(buildPlayerOptions);
		}
	}
}