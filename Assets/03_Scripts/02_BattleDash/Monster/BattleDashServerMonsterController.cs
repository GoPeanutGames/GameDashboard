#if SERVER
using PeanutDashboard._02_BattleDash.State;
#endif
using PeanutDashboard.Shared.Logging;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Monster
{
	public class BattleDashServerMonsterController: NetworkBehaviour, IBattleDashDamageable, IBattleDashFactionable
	{
		public BattleDashFactionType BattleDashFactionType => _battleDashFactionType;
		
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private BattleDashFactionType _battleDashFactionType;
		
		[SerializeField]
		private BattleDashMonsterType _battleDashMonsterType;
		
		[SerializeField]
		private AudioClip _dieSfxClip;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private NetworkAnimator _networkAnimator;
		
		[SerializeField]
		private Collider2D _collider2D;
		
		[SerializeField]
		private bool _destroyed;
		
		[SerializeField]
		private int _hp;

		private static readonly int Hit = Animator.StringToHash("Hit");
		private static readonly int Die = Animator.StringToHash("Die");
		
#if SERVER
		private void Awake()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(Awake)}");
			_networkAnimator = GetComponent<NetworkAnimator>();
			_collider2D = GetComponent<Collider2D>();
			_hp = _battleDashMonsterType.monsterHp;
		}

		private void Update()
		{
			if (BattleDashServerGameState.isPaused){
				return;
			}
			this.transform.position += Vector3.left * (_battleDashMonsterType.monsterSpeed * NetworkManager.ServerTime.FixedDeltaTime);
			if (!_destroyed && this.transform.position.x < -60){
				_destroyed = true;
				this.GetComponent<NetworkObject>().Despawn();
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(OnTriggerEnter2D)} - {other.name}");
			if (_destroyed){
				LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(OnTriggerEnter2D)} - we're already killed returning");
				return;
			}
			IBattleDashFactionable otherFaction = other.GetComponent<IBattleDashFactionable>();
			if (otherFaction == null || otherFaction.BattleDashFactionType == this._battleDashFactionType){
				LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(OnTriggerEnter2D)} - no faction or faction the same returning");
				return;
			}
			IBattleDashDamageable otherDamageable = other.GetComponent<IBattleDashDamageable>();
			if (otherDamageable == null){
				Debug.Log($"{nameof(BattleDashServerMonsterController)}::{nameof(OnTriggerEnter2D)} - other object can't be damaged, returning");
				return;
			}
			otherDamageable.TakeDamage(_battleDashMonsterType.damageDealt);
			_destroyed = true;
			TakeDamage(100);
		}
#endif
		public void TakeDamage(int amount)
		{
			if (NetworkManager.IsClient){
				return;
			}
			LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(TakeDamage)}");
			_hp -= amount;
			_networkAnimator.SetTrigger(Hit);
			if (_hp <= 0){
				_destroyed = true;
				_collider2D.enabled = false;
				_networkAnimator.SetTrigger(Die);
			}
		}

		public void TriggerDeathSfx()
		{
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_dieSfxClip, 1);
		}

		public void Remove()
		{
#if SERVER
			LoggerService.LogInfo($"{nameof(BattleDashServerMonsterController)}::{nameof(Remove)}");
			this.GetComponent<NetworkObject>().Despawn();
#endif
		}
	}
}