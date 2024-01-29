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
		
		[Header("Debug Dynamic")]
		[SerializeField]
		private EnvironmentModel _currentEnvironment;

		protected override void Awake()
		{
			base.Awake();
			_currentEnvironment = _currentGameConfig.currentEnvironmentModel;
			LoggerService.LogInfo($"{nameof(EnvironmentManager)}::{nameof(Awake)} - Logs: {_currentEnvironment.allowLogs}");
		}

		public string GetServerUrl()
		{
			return _currentEnvironment.serverUrl;
		}

		public string GetUnityEnvironmentName()
		{
			return _currentEnvironment.unityEnvironmentName;
		}

		public string GetCurrentPublicKey()
		{
			return string.Join("\n", _currentEnvironment.publicKey);
		}

		public bool IsRSAActive()
		{
			return _currentEnvironment.useRSA;
		}

		public bool IsLoggingEnabled()
		{
			return _currentEnvironment.allowLogs;
		}

		public bool IsDev()
		{
			return _currentEnvironment.isDev;
		}
	}
}