using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard.UnityServer.Events
{
	public static class ServerEvents
	{
		private static UnityAction _shutDownServer;

		public static event UnityAction ShutDownServer
		{
			add => _shutDownServer += value;
			remove => _shutDownServer -= value;
		}

		public static void RaiseShutDownServerEvent()
		{
			if (_shutDownServer == null){
				LoggerService.LogWarning($"{nameof(ServerEvents)}::{nameof(RaiseShutDownServerEvent)} raised, but nothing picked it up");
				return;
			}
			_shutDownServer.Invoke();
		}
	}
}