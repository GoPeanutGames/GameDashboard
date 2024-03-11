#if !SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.State;
#endif
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashClientBulletSpawnPoint: MonoBehaviour
	{
#if !SERVER
		private void Update()
		{
			if (BattleDashServerGameState.isPaused){
				return;
			}
			BattleDashClientActionEvents.RaiseUpdatePlayerBulletSpawnPointEvent(this.transform.position);
		}
#endif
	}
}