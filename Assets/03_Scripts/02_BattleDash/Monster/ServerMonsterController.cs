using System;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard._02_BattleDash.State;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Monster
{
	public class ServerMonsterController: NetworkBehaviour, IDamageable, IFactionable
	{
		public FactionType FactionType => _factionType;
		
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private FactionType _factionType;
		
		[SerializeField]
		private MonsterType _monsterType;
		
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
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(Awake)}");
			_networkAnimator = GetComponent<NetworkAnimator>();
			_collider2D = GetComponent<Collider2D>();
			_hp = _monsterType.monsterHp;
		}

		private void Update()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			this.transform.position += Vector3.left * (_monsterType.monsterSpeed * NetworkManager.ServerTime.FixedDeltaTime);
			if (!_destroyed && this.transform.position.x < -60){
				_destroyed = true;
				this.GetComponent<NetworkObject>().Despawn();
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(OnTriggerEnter2D)} - {other.name}");
			if (_destroyed){
				Debug.Log($"{nameof(ServerMonsterController)}::{nameof(OnTriggerEnter2D)} - we're already killed returning");
				return;
			}
			IFactionable otherFaction = other.GetComponent<IFactionable>();
			if (otherFaction == null || otherFaction.FactionType == this._factionType){
				Debug.Log($"{nameof(ServerMonsterController)}::{nameof(OnTriggerEnter2D)} - no faction or faction the same returning");
				return;
			}
			IDamageable otherDamageable = other.GetComponent<IDamageable>();
			if (otherDamageable == null){
				Debug.Log($"{nameof(ServerMonsterController)}::{nameof(OnTriggerEnter2D)} - other object can't be damaged, returning");
				return;
			}
			otherDamageable.TakeDamage(_monsterType.damageDealt);
			_destroyed = true;
			TakeDamage(100);
		}
#endif
		public void TakeDamage(int amount)
		{
			if (NetworkManager.IsClient){
				return;
			}
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(TakeDamage)}");
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
			ClientBattleDashAudioEvents.RaisePlaySfxEvent(_dieSfxClip, 1);
		}

		public void Remove()
		{
#if SERVER
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(Remove)}");
			this.GetComponent<NetworkObject>().Despawn();
#endif
		}
	}
}