using System;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
	public class UnityServerInit: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameInfo _gameInfo;

		[SerializeField]
		private GameObject _networkManager;

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
            //#if SERVER
            //			LoggerService.LogInfo($"{nameof(UnityServerInit)}::{nameof(OnSpawnServer)}");
            //			Instantiate(_networkManager);
            //			ServerEvents.RaiseStartServerEvent(_gameInfo);
            //#endif
        }
    }
}