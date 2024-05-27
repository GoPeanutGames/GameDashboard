using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerHealth: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _maxHealth;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _currentHealth;

		private void Awake()
		{
			_currentHealth = _maxHealth;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "Enemy"){
				Destroy(other.gameObject);
				_currentHealth -= 10f;
				if (_currentHealth <= 0){
					RobotRampagePlayerEvents.RaisePlayerKilledEvent();
				}
			}
		}
	}
}