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
	public class BattleDashServerPlayerHealth: NetworkBehaviour, IBattleDashDamageable, IBattleDashFactionable
	{
		public BattleDashFactionType BattleDashFactionType => _battleDashFactionType;
		
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private BattleDashFactionType _battleDashFactionType;

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
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerHealth)}::{nameof(TakeDamage)}");
			_health -= amount;
			_networkAnimator.SetTrigger(Hit);
			SendClientPlayerDamaged_ClientRpc();
			if (_health <= 0){
				LoggerService.LogInfo($"{nameof(BattleDashServerPlayerHealth)}::{nameof(TakeDamage)} - die");
				_networkAnimator.SetTrigger(Die);
				SendClientPlayerDied_ClientRpc();
				BackendAuthenticationManager.SubmitGameEnd(false);
			}
#endif
        }

        [ClientRpc]
		private void SendClientPlayerDied_ClientRpc()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerHealth)}::{nameof(SendClientPlayerDied_ClientRpc)}");
			_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
			BattleDashClientUIEvents.RaiseShowGameOverEvent();
		}
		
		[ClientRpc]
		private void SendClientPlayerDamaged_ClientRpc()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerPlayerHealth)}::{nameof(SendClientPlayerDamaged_ClientRpc)}");
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_damagedClip, 1f);
		}
	}
}