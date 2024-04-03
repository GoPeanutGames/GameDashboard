using PeanutDashboard.Init;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard.UnityServer.Events
{
	public static class ServerEvents
	{
		private static UnityAction _shutDownServer;
		private static UnityAction _spawnServer;
		private static UnityAction<GameInfo> _startServer;

		public static event UnityAction ShutDownServer
		{
			add => _shutDownServer += value;
			remove => _shutDownServer -= value;
		}
		
		public static event UnityAction SpawnServer
		{
			add => _spawnServer += value;
			remove => _spawnServer -= value;
		}
		
		public static event UnityAction<GameInfo> StartServer
		{
			add => _startServer += value;
			remove => _startServer -= value;
		}

		public static void RaiseShutDownServerEvent()
		{
			if (_shutDownServer == null){
				LoggerService.LogWarning($"{nameof(ServerEvents)}::{nameof(RaiseShutDownServerEvent)} raised, but nothing picked it up");
				return;
			}
			_shutDownServer.Invoke();
		}
		
		public static void RaiseSpawnServerEvent()
		{
			if (_spawnServer == null){
				LoggerService.LogWarning($"{nameof(ServerEvents)}::{nameof(RaiseSpawnServerEvent)} raised, but nothing picked it up");
				return;
			}
			_spawnServer.Invoke();
		}

		public static void RaiseStartServerEvent(GameInfo gameInfo)
		{
			if (_startServer == null){
				LoggerService.LogWarning($"{nameof(ServerEvents)}::{nameof(RaiseStartServerEvent)} raised, but nothing picked it up");
				return;
			}
			_startServer.Invoke(gameInfo);
		}
	}
}