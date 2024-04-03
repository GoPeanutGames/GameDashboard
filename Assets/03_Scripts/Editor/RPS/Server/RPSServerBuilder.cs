using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CustomEditor(typeof(RPSServerBuilder))]
	public class RPSServerBuilder : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Build/RockPaperScissors/Server/Development Testing")]
		public static void BuildForServerDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}
		
		[MenuItem("PeanutDashboard/Build/RockPaperScissors/Server/Development Release")]
		public static void BuildForServerDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
		}

		[MenuItem("PeanutDashboard/Build/RockPaperScissors/Server/Production Testing")]
		public static void BuildForServerProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to build for production?", "Build", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				BuildForServer(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId);
			}
		}

		[MenuItem("PeanutDashboard/Build/RockPaperScissors/Server/Production Release")]
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
			scenesInBuild.Add(ProjectDatabase.Instance.rockPaperScissorsSceneConfig.scenePath);
			BundledAssetGroupSchema schema = ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.GetSchema<BundledAssetGroupSchema>();
			ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.Settings.activeProfileId = addressableProfileId;
			var buildInfo = ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.BuildPath");
			var loadInfo = ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.Settings.profileSettings.GetProfileDataByName("Local.LoadPath");
			schema.BuildPath.SetVariableById(ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.Settings, buildInfo.Id);
			schema.LoadPath.SetVariableById(ProjectDatabase.Instance.rockPaperScissorsSceneConfig.group.Settings, loadInfo.Id);
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