using System;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
	public class BattleDashUnityServerInit : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameInfo _gameInfo;

		private void OnEnable()
		{
			ServerEvents.SpawnServer += OnSpawnServer;
		}

		private void OnDisable()
		{
			ServerEvents.SpawnServer -= OnSpawnServer;
		}


		private void Start()
		{
			OnSpawnServer();
		}

		private void OnSpawnServer()
		{
            LoggerService.LogInfo($"{nameof(UnityServerInit)}::{nameof(OnSpawnServer)}");
            ServerEvents.RaiseStartServerEvent(_gameInfo);
        }
    }
}