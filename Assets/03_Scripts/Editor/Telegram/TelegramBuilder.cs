using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build;
using UnityEditor.Compilation;
using UnityEditor.WebGL;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	public class TelegramBuilder : UnityEditor.Editor
	{
		[MenuItem("PeanutDashboard/Build/Telegram/RockPaperScissors/Development Testing")]
		public static void ConfigForClientTelegramRPSDevTest()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevTesting();
			BuildForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId, TelegramGame.RockPaperScissors);
		}

		[MenuItem("PeanutDashboard/Build/Telegram/RockPaperScissors/Development Release")]
		public static void ConfigForClientTelegramRPSDevRelease()
		{
			ProjectDatabase.Instance.gameConfig.ConfigureForDevRelease();
			BuildForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId, TelegramGame.RockPaperScissors);
		}

		[MenuItem("PeanutDashboard/Build/Telegram/RockPaperScissors/Production Testing")]
		public static void ConfigForClientTelegramRPSProdTest()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for Telegram production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdTesting();
				BuildForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId, TelegramGame.RockPaperScissors);
			}
		}

		[MenuItem("PeanutDashboard/Build/Telegram/RockPaperScissors/Production Release")]
		public static void ConfigForClientTelegramRPSProdRelease()
		{
			if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to config for Telegram production?", "Config", "Cancel")){
				ProjectDatabase.Instance.gameConfig.ConfigureForProdRelease();
				BuildForClientTelegram(ProjectDatabase.Instance.gameConfig.currentEnvironmentModel.unityAddressablesProfileId, TelegramGame.RockPaperScissors);
			}
		}

		private static List<string> ConfigForRPS(string addressableProfileId)
		{
			List<string> scenesInBuild = new List<string>();
			GameSceneConfig rpsGameSceneConfig = ProjectDatabase.Instance.rockPaperScissorsSceneConfig;
			scenesInBuild.Add(rpsGameSceneConfig.scenePath);
			AddressableAssetGroup group = rpsGameSceneConfig.group;
			BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
			group.Settings.activeProfileId = addressableProfileId;
			var buildInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.BuildPath");
			var loadInfo = group.Settings.profileSettings.GetProfileDataByName("Remote.LoadPath");
			schema.BuildPath.SetVariableById(group.Settings, buildInfo.Id);
			schema.LoadPath.SetVariableById(group.Settings, loadInfo.Id);
			return scenesInBuild;
		}
		
		private static void BuildForClientTelegram(string addressableProfileId, TelegramGame telegramGame)
		{
			// Get main folder path.
			string parentFolderPath = EditorUtility.SaveFolderPanel("Choose the main folder", "", "");

			if (string.IsNullOrWhiteSpace(parentFolderPath)){
				Debug.Log(
					$"{nameof(ServerBuilder)}::{nameof(BuildForClientTelegram)}:: parent folder not selected, returning!");

				return;
			}
			List<string> scenesInBuild = new List<string>();
			switch (telegramGame){
				case TelegramGame.RockPaperScissors:
					scenesInBuild = ConfigForRPS(addressableProfileId);
					break;
			}
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
			PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.WebGL, "");
			// PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;
			PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
			PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.WebGL, ManagedStrippingLevel.High);
			UserBuildSettings.codeOptimization = WasmCodeOptimization.DiskSizeLTO;
			AddressableAssetSettings.BuildPlayerContent();
			string folderName = "Telegram_"+telegramGame;
			BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
			{
				scenes = scenesInBuild.ToArray(),
				locationPathName = Path.Combine(parentFolderPath, folderName),
				target = BuildTarget.WebGL,
			};
			BuildPipeline.BuildPlayer(buildPlayerOptions);
		}
	}
}