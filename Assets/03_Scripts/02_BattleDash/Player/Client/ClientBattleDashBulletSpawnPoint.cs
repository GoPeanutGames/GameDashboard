#if !SERVER
using PeanutDashboard._02_BattleDash.Events;
#endif
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class ClientBattleDashBulletSpawnPoint: MonoBehaviour
	{
#if !SERVER
		private void Update()
		{
			ClientActionEvents.RaiseUpdatePlayerBulletSpawnPointEvent(this.transform.position);
		}
#endif
	}
}