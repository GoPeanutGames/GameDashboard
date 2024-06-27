using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerHealth: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _maxHealth;
		
		[SerializeField]
		private float _currentHealth;

		private void Awake()
		{
			_currentHealth = RobotRampageCharacterStatsService.GetMaxHp();
			_maxHealth = RobotRampageCharacterStatsService.GetMaxHp();
		}

		private void OnEnable()
		{
			RobotRampageUpgradeEvents.OnRefreshStats += RefreshHealth;
		}

		private void OnDisable()
		{
			RobotRampageUpgradeEvents.OnRefreshStats -= RefreshHealth;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "Enemy"){
				Destroy(other.gameObject);
				_currentHealth -= 10f;
				RobotRampageUIEvents.RaiseUpdatePlayerHealthBarEvent(_currentHealth, _maxHealth);
				if (_currentHealth <= 0){
					RobotRampagePlayerEvents.RaisePlayerKilledEvent();
					RobotRampagePopupEvents.RaiseOpenDefeatPopupEvent();
				}
			}
		}

		private void RefreshHealth()
		{
			Debug.Log($"{nameof(RobotRampagePlayerHealth)}::{nameof(RefreshHealth)}");
			float newMaxHealth = RobotRampageCharacterStatsService.GetMaxHp();
			if (newMaxHealth > _maxHealth){
				float diff = newMaxHealth - _maxHealth;
				_maxHealth = newMaxHealth;
				_currentHealth += diff;
				RobotRampageUIEvents.RaiseUpdatePlayerHealthBarEvent(_currentHealth, _maxHealth);
				Debug.Log($"{nameof(RobotRampagePlayerHealth)}::{nameof(RefreshHealth)} - health changed by: {diff}");
			}
		}
	}
}