using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class OverlayCanvasController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _damageIndicatorPrefab;
		
		private void OnEnable()
		{
			RobotRampageOverlayUIEvents.OnSpawnDamageIndicator += OnSpawnDamageIndicator;
		}

		private void OnDisable()
		{
			RobotRampageOverlayUIEvents.OnSpawnDamageIndicator -= OnSpawnDamageIndicator;
		}

		private void OnSpawnDamageIndicator(Vector3 worldPosition, float damage)
		{
			GameObject indicator = Instantiate(_damageIndicatorPrefab, worldPosition, Quaternion.identity, this.transform);
			indicator.GetComponent<RobotRampageDamageIndicator>().SetIndicator(damage);
		}
	}
}