using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class ClientActionEvents
	{
		private static UnityAction<Vector2> _updatePlayerAim;

		public static event UnityAction<Vector2> OnUpdatePlayerAim
		{
			add => _updatePlayerAim += value;
			remove => _updatePlayerAim -= value;
		}
		public static void RaiseUpdatePlayerAimEvent(Vector2 worldPosition)
		{
			if (_updatePlayerAim == null){
				LoggerService.LogWarning($"{nameof(ServerSpawnEvents)}::{nameof(RaiseUpdatePlayerAimEvent)} raised, but nothing picked it up");
				return;
			}
			_updatePlayerAim.Invoke(worldPosition);
		}
	}
}