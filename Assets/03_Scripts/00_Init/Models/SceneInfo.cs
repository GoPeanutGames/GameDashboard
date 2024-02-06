using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Init
{
	[CreateAssetMenu(
		fileName = nameof(SceneInfo) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardModels + nameof(SceneInfo)
	)]
	public class SceneInfo : ScriptableObject
	{
		public string name;
		public string key;
		public string label;
	}
}