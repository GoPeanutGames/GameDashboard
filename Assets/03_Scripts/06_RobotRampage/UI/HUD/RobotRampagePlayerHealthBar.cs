using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerHealthBar: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Image _healthImage;
		
		private void OnEnable()
		{
			RobotRampageUIEvents.UpdatePlayerHealthBar += OnUpdateHealthBar;
		}

		private void OnDisable()
		{
			RobotRampageUIEvents.UpdatePlayerHealthBar -= OnUpdateHealthBar;
		}

		private void OnUpdateHealthBar(float current, float max)
		{
			_healthImage.fillAmount = current / max;
		}
	}
}