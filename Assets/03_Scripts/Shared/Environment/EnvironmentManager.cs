using MetaMask.Unity;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Environment
{
    public class EnvironmentManager: MonoSingleton<EnvironmentManager>
    {
        [SerializeField] private EnvironmentModel _currentEnvironment;

        private void Start()
        {
            Debug.Log($"{nameof(EnvironmentManager)}::{nameof(Start)} - Logs: {_currentEnvironment.allowLogs}");
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

        public MetaMaskConfig GetMetamaskConfig()
        {
            return _currentEnvironment.metaMaskConfig;
        }
    }
}