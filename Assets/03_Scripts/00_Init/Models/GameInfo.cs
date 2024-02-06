using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Init
{
	[CreateAssetMenu(
		fileName = nameof(GameInfo) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardModels + nameof(GameInfo)
	)]
	public class GameInfo: ScriptableObject
	{
		public List<SceneInfo> gameScenes;
		public SceneInfo gameMainScene;
		public SceneInfo gamePlayScene;
		public SceneInfo networkEntryPointScene;
		public string matchmakerGameLabel;
	}
}