using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(ServerBuilder))]
	public class ServerBuilder: UnityEditor.Editor
	{
		// private static readonly string ServerEntryPointScene = "EntryPoint";

		[MenuItem("PeanutDashboard/Build Server Development")]
		public static void BuildForServerDev()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDev();
			BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Build Server Production")]
		public static void BuildForServerProd()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProd();
				BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}
		
		private static void BuildForServer(string addressableProfileId)
		{
			// Get main folder path.
			string parentFolderPath = EditorUtility.SaveFolderPanel("Choose the main folder", "", "");
			
			if (string.IsNullOrWhiteSpace(parentFolderPath)) {
				Debug.Log(
					$"{nameof(ServerBuilder)}::{nameof(BuildForServer)}:: parent folder not selected, returning!");
			
				return;
			}
			
			List<string> scenesInBuild = new List<string>();
			// scenesInBuild.Add($"Assets/01_Scenes/Server/{ServerEntryPointScene}.unity");
			foreach (GameSceneConfig gameSceneConfig in ProjectDatabase.Instance.serverGameSceneConfigs){
				scenesInBuild.Add(gameSceneConfig.scenePath);
				BundledAssetGroupSchema schema = gameSceneConfig.group.GetSchema<BundledAssetGroupSchema>();
				gameSceneConfig.group.Settings.activeProfileId = addressableProfileId;
				var buildInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.BuildPath");
				var loadInfo = gameSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.LoadPath");
				schema.BuildPath.SetVariableById(gameSceneConfig.group.Settings, buildInfo.Id);
				schema.LoadPath.SetVariableById(gameSceneConfig.group.Settings, loadInfo.Id);
			}
			AddressableAssetSettings.BuildPlayerContent();
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Server, "SERVER");
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