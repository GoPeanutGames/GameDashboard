using PeanutDashboard.Shared.Config;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Environment
{
	public class EnvironmentManager : MonoSingleton<EnvironmentManager>
	{
		[Header("Set in Inspector")]
		[SerializeField]
		private GameConfig _currentGameConfig;
		
		protected override void Awake()
		{
			base.Awake();
			Debug.Log($"{nameof(EnvironmentManager)}::{nameof(Awake)} - Peanut Dashboard version: {Application.version}");
			LoggerService.LogInfo($"{nameof(EnvironmentManager)}::{nameof(Awake)} - Logs: {_currentGameConfig.currentEnvironmentModel.allowLogs}");
		}

		public GameConfig GetGameConfig()
		{
			return _currentGameConfig;
		}
		
		public string GetServerUrl()
		{
			return _currentGameConfig.currentEnvironmentModel.serverUrl;
		}

		public string GetUnityEnvironmentName()
		{
			return _currentGameConfig.currentEnvironmentModel.unityEnvironmentName;
		}

		public string GetCurrentPublicKey()
		{
			return string.Join("\n", _currentGameConfig.currentEnvironmentModel.publicKey);
		}

		public bool IsRSAActive()
		{
			return _currentGameConfig.currentEnvironmentModel.useRSA;
		}

		public bool IsLoggingEnabled()
		{
			return _currentGameConfig.currentEnvironmentModel.allowLogs;
		}

		public bool IsDev()
		{
			return _currentGameConfig.currentEnvironmentModel.isDev;
		}
	}
}