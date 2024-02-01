using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Events;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
	public class UnityServerConnectionHandler: MonoBehaviour
	{
		private void OnEnable()
		{
			LoggerService.LogInfo($"{nameof(UnityServerConnectionHandler)}::{nameof(OnEnable)}");
			UnityServerStartUp.ServerInstance += SetupServerEvents;
		}

		private void SetupServerEvents()
		{
			LoggerService.LogInfo($"{nameof(UnityServerConnectionHandler)}::{nameof(SetupServerEvents)}");
			NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
		}

		private void OnClientDisconnected(ulong id)
		{
			LoggerService.LogInfo($"{nameof(UnityServerConnectionHandler)}::{nameof(OnClientDisconnected)}");
			ServerEvents.RaiseShutDownServerEvent();
		}

		private void OnDisable()
		{
			LoggerService.LogInfo($"{nameof(UnityServerConnectionHandler)}::{nameof(OnDisable)}");
			UnityServerStartUp.ServerInstance -= SetupServerEvents;
		}
	}
}