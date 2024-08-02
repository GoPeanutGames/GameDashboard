using PeanutDashboard.Shared.Environment;
using UnityEngine;

namespace PeanutDashboard.Shared.Logging
{
	public static class LoggerService
	{
		public static void LogInfo(string log)
		{
			if (EnvironmentManager.Instance == null){
				Debug.Log(log);
			}
			else if (EnvironmentManager.Instance.LoggingEnabled){
				Debug.Log(log);
			}
		}

		public static void LogWarning(string log)
		{
			if (EnvironmentManager.Instance == null){
				Debug.LogWarning(log);
			}
			else if (EnvironmentManager.Instance.LoggingEnabled){
				Debug.LogWarning(log);
			}
		}

		public static void LogError(string log)
		{
			if (EnvironmentManager.Instance == null){
				Debug.LogError(log);
			}
			else if (EnvironmentManager.Instance.LoggingEnabled){
				Debug.LogError(log);
			}
		}
	}
}