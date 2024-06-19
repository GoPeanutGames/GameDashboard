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
		private Transform _particleTransform;

		[SerializeField]
		private Transform _colliderTransform;
		
		[SerializeField]
		private WeaponType _weaponType;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private List<RobotRampageMonsterController> _enemiesToDamage = new();
		
		[SerializeField]
		private List<RobotRampageMonsterController> _enemiesToRemove = new();
		
		[SerializeField]
		private float _timeToShoot;
		
		[SerializeField]
		private DamageType _damageType;

		[SerializeField]
		private bool _looping;

		[SerializeField]
		private float _currentAOE = 1;

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
			_timeToShoot = RobotRampageWeaponStatsService.GetWeaponCooldown(_weaponType);
		}

		private void Update()
		{
			_timeToShoot -= Time.deltaTime;
			if (_timeToShoot <= 0){
				DamageAllEnemies();
			}
			CheckForAOEChange();
		}

		private void CheckForAOEChange()
		{
			if (Mathf.Approximately(RobotRampageWeaponStatsService.GetWeaponAOE(_weaponType), _currentAOE)){
				return;
			}
			_currentAOE = RobotRampageWeaponStatsService.GetWeaponAOE(_weaponType);
			_colliderTransform.localScale = new Vector3( _colliderTransform.localScale.x * _currentAOE, _colliderTransform.localScale.y * _currentAOE, _colliderTransform.localScale.z * _currentAOE);
			_particleTransform.localScale = new Vector3( _particleTransform.localScale.x * _currentAOE, _particleTransform.localScale.y * _currentAOE, _particleTransform.localScale.z * _currentAOE);
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
			_timeToShoot = RobotRampageWeaponStatsService.GetWeaponCooldown(_weaponType);
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