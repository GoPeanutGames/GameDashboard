using System;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Shared.Logging;
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
		private Animator _animator;
		
		[SerializeField]
		private NetworkAnimator _networkAnimator;
		
		[SerializeField]
		private AudioClip _damagedClip;
		
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
			SendClientPlayerDamaged_ClientRpc();
			if (_health <= 0){
				Debug.Log($"{nameof(ServerBattleDashPlayerHealth)}::{nameof(TakeDamage)} - die");
				_networkAnimator.SetTrigger(Die);
				SendClientPlayerDied_ClientRpc();
			}
#endif
		}

		[ClientRpc]
		private void SendClientPlayerDied_ClientRpc()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerHealth)}::{nameof(SendClientPlayerDied_ClientRpc)}");
			_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
			BattleDashClientUIEvents.RaiseShowGameOverEvent();
		}
		
		[ClientRpc]
		private void SendClientPlayerDamaged_ClientRpc()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashPlayerHealth)}::{nameof(SendClientPlayerDamaged_ClientRpc)}");
			BattleDashAudioEvents.RaisePlaySfxEvent(_damagedClip, 1f);
		}
	}
}