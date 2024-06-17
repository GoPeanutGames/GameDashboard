using System;
using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageFlamethrowerWeapon: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		
		private RobotRampageColliderTrigger _colliderTrigger;
		[SerializeField]
		private WeaponType _weaponType;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private List<RobotRampageMonsterController> _enemiesToDamage = new();
		
		[SerializeField]
		private List<RobotRampageMonsterController> _enemiesToRemove = new();
		
		[SerializeField]
		private int _shotsPerSecond;
		
		[SerializeField]
		private float _timeToShoot;
		
		[SerializeField]
		private DamageType _damageType;

		[SerializeField]
		private bool _looping;

		private void OnEnable()
		{
			_colliderTrigger.AddListenerOnEnter(OnTriggerEntered);
			_colliderTrigger.AddListenerOnExit(OnTriggerExited);
		}

		private void OnDisable()
		{
			_colliderTrigger.RemoveListenerOnEnter(OnTriggerEntered);
			_colliderTrigger.RemoveListenerOnExit(OnTriggerExited);
		}

		private void Start()
		{
			_damageType = RobotRampageWeaponStatsService.GetWeaponDamageType(_weaponType);
			_timeToShoot = 1f / _shotsPerSecond;
		}

		private void Update()
		{
			_timeToShoot -= Time.deltaTime;
			if (_timeToShoot <= 0){
				DamageAllEnemies();
			}
		}

		private void DamageAllEnemies()
		{
			_looping = true;
			foreach (RobotRampageMonsterController robotRampageMonsterController in _enemiesToDamage){
				float damage = RobotRampageWeaponStatsService.GetWeaponDamage(_weaponType, _damageType);
				robotRampageMonsterController.Damage(damage);
			}
			_looping = false;
			foreach (RobotRampageMonsterController robotRampageMonsterController in _enemiesToRemove){
				_enemiesToDamage.Remove(robotRampageMonsterController);
			}
			_enemiesToRemove.Clear();
			_timeToShoot = 1f / _shotsPerSecond;
		}

		private void OnTriggerEntered(Collider2D other)
		{
			if (other.tag.Equals("Enemy")){
				_enemiesToDamage.Add(other.gameObject.GetComponent<RobotRampageMonsterController>());
			}
		}

		private void OnTriggerExited(Collider2D other)
		{
			if (other.tag.Equals("Enemy") && !_looping){
				_enemiesToDamage.Remove(other.gameObject.GetComponent<RobotRampageMonsterController>());
			}
			else if(other.tag.Equals("Enemy") && _looping){
				_enemiesToRemove.Add(other.gameObject.GetComponent<RobotRampageMonsterController>());
			}
		}
	}
}