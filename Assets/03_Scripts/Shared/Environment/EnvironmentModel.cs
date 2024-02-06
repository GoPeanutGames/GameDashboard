using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.Shared.Environment
{
	[CreateAssetMenu(
		fileName = nameof(EnvironmentModel) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.DashboardModels + nameof(EnvironmentModel)
	)]
	public class EnvironmentModel : ScriptableObject
	{
		public bool isDev;
		public bool allowLogs;
		public bool useRSA;
		public string serverUrl;
		public string unityEnvironmentName;
		public string unityAddressablesProfileId;
		public List<string> publicKey;
	}
}