using System;
using PeanutDashboard._02_BattleDash.Interaction;
using PeanutDashboard._02_BattleDash.Model;
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

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private NetworkAnimator _networkAnimator;
		
		[SerializeField]
		private bool _destroyed;
		
		[SerializeField]
		private bool _dying;
		
		[SerializeField]
		private int _hp;

		[SerializeField]
		private float _downDirectionChange;

		private static readonly int Hit = Animator.StringToHash("Hit");
		
#if SERVER
		private void Awake()
		{
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(Awake)}");
			_networkAnimator = GetComponent<NetworkAnimator>();
			_hp = _monsterType.monsterHp;
			_downDirectionChange = 0;
		}

		private void Update()
		{
			this.transform.position += Vector3.left * (_monsterType.monsterSpeed * NetworkManager.ServerTime.FixedDeltaTime) + Vector3.down * (_downDirectionChange * NetworkManager.ServerTime.FixedDeltaTime);
			if (!_destroyed && this.transform.position.x < -60){
				_destroyed = true;
				this.GetComponent<NetworkObject>().Despawn();
			}
			if (_dying){
				_downDirectionChange += 9.81f * NetworkManager.ServerTime.FixedDeltaTime;
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
				_dying = true;
				Invoke(nameof(Die), 3f);
			}
		}

		private void Die()
		{
			Debug.Log($"{nameof(ServerMonsterController)}::{nameof(Die)}");
			_destroyed = true;
			this.GetComponent<NetworkObject>().Despawn();
		}
	}
}