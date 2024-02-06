using PeanutDashboard.Utils.Misc;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace PeanutDashboard.Editor
{
	[CreateAssetMenu(
		fileName = nameof(GameSceneConfig) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardConfigs + nameof(GameSceneConfig)
	)]
	public class GameSceneConfig: ScriptableObject
	{
		public string scenePath;
		public AddressableAssetGroup group;
	}
}