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
	public class ServerBuilder : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Build/Server/Development Testing")]
		public static void BuildForServerDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Build/Server/Development Release")]
		public static void BuildForServerDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Build/Server/Production Testing")]
		public static void BuildForServerProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		[MenuItem("PeanutDashboard/Build/Server/Production Release")]
		public static void BuildForServerProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		private static void BuildForServer(string addressableProfileId)
		{
			// Get main folder path.
			string parentFolderPath = EditorUtility.SaveFolderPanel("Choose the main folder", "", "");

			if (string.IsNullOrWhiteSpace(parentFolderPath)){
				Debug.Log(
					$"{nameof(ServerBuilder)}::{nameof(BuildForServer)}:: parent folder not selected, returning!");

				return;
			}

			List<string> scenesInBuild = new List<string>();
			scenesInBuild.Add($"Assets/01_Scenes/Server/EntryPointScene.unity");
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