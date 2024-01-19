﻿using PeanutDashboard.Shared.Environment;
using UnityEngine;

namespace PeanutDashboard.Shared.Logging
{
    public static class LoggerService
    {
        public static void LogInfo(string log)
        {
            if (EnvironmentManager.Instance.IsLoggingEnabled())
            {
                Debug.Log(log);
            }
        }
        
        public static void LogWarning(string log)
        {
            if (EnvironmentManager.Instance.IsLoggingEnabled())
            {
                Debug.LogWarning(log);
            }
        }
        
        public static void LogError(string log)
        {
            if (EnvironmentManager.Instance.IsLoggingEnabled())
            {
                Debug.LogError(log);
            }
        }
    }
}