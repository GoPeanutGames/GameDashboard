using System.Collections.Generic;
using System.IO;
using PeanutDashboard.Shared.Config;
using PeanutDashboard.Utils.Misc;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CreateAssetMenu(
		fileName = nameof(ProjectDatabase) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardConfigs + nameof(ProjectDatabase)
	)]
	public class ProjectDatabase: ScriptableObject
	{
		private const string FilePath = "Assets/05_Scriptables/Editor/" +
		                                nameof(ProjectDatabase) + ExtensionNames.DotAsset;
		private const string MenuItemName = "Project Database";
		
		[MenuItem("PeanutDashboard/Tools/" + MenuItemName)]
		private static void OpenEnvironmentDatabase() {
			if (!Instance) {
				Debug.LogWarning($"Could not ping asset= {Instance}");

				return;
			}

			EditorGUIUtility.PingObject(Instance);
			Selection.activeObject = Instance;
			EditorUtility.FocusProjectWindow();
		}
		
		private static ProjectDatabase instance;

		public List<GameSceneConfig> serverGameSceneConfigs;
		public List<AddressableAssetGroup> addressableGroups;
		public GameConfig gameConfig;
		
		public static ProjectDatabase Instance {
			get {
				if (!instance) {
					instance = AssetDatabase.LoadAssetAtPath<ProjectDatabase>(FilePath);
				}

				if (instance) {
					return instance;
				}

				instance = CreateInstance<ProjectDatabase>();

				var directoryName = Path.GetDirectoryName(FilePath);
				var directionPath = Path.Combine(Application.dataPath, directoryName!);

				if (!Directory.Exists(directionPath)) {
					Directory.CreateDirectory(directionPath);
				}

				AssetDatabase.CreateAsset(instance, FilePath);

				return instance;
			}
		}
	}
}