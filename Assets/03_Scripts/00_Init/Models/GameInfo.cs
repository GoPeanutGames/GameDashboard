using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Init
{
	[CreateAssetMenu(
		fileName = nameof(GameInfo) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.Models + nameof(GameInfo)
	)]
	public class GameInfo: ScriptableObject
	{
		public List<SceneInfo> gameScenes;
		public SceneInfo gameStartScene;
		public SceneInfo networkStartScene;
		public SceneInfo networkEntryPointScene;
		public string matchmakerGameLabel;
	}
}