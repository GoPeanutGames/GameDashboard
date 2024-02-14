﻿#if !SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.State;
#endif
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class ClientBattleDashBulletSpawnPoint: MonoBehaviour
	{
#if !SERVER
		private void Update()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			ClientActionEvents.RaiseUpdatePlayerBulletSpawnPointEvent(this.transform.position);
		}
#endif
	}
}