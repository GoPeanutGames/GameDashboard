using System;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Server
{
	public class ServerBattleDashPlayerHealth: NetworkBehaviour, IDamageable, IFactionable
	{
		public FactionType FactionType => _factionType;
		
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private FactionType _factionType;

		[SerializeField]
		private NetworkAnimator _networkAnimator;
		
		[SerializeField]
		private int _health;
		
		private static readonly int Hit = Animator.StringToHash("Hit");
		private static readonly int Die = Animator.StringToHash("Die");
		
		public void TakeDamage(int amount)
		{
#if SERVER
			Debug.Log($"{nameof(ServerBattleDashPlayerHealth)}::{nameof(TakeDamage)}");
			_health -= amount;
			_networkAnimator.SetTrigger(Hit);
			if (_health <= 0){
				Debug.Log($"{nameof(ServerBattleDashPlayerHealth)}::{nameof(TakeDamage)} - die");
				_networkAnimator.SetTrigger(Die);
				//TODO: animation event when die is done to stop the game (pause) and show game over overlay
			}
#endif
		}

	}
}