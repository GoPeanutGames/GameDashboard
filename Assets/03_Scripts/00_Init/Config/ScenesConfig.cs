using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Init
{
	[CreateAssetMenu(
		fileName = nameof(ScenesConfig) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.Configs + nameof(ScenesConfig)
	)]
	public class ScenesConfig : ScriptableObject
	{
		public List<SceneInfo> sceneInfos;
	}
}