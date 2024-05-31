using System;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePopupsController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _victoryPopup;

		[SerializeField]
		private GameObject _defeatPopup;

		private void OnEnable()
		{
			RobotRampagePopupEvents.OnOpenVictoryPopup += OpenVictoryPopup;
			RobotRampagePopupEvents.OnOpenDefeatPopup += OpenDefeatPopup;
		}

		private void OnDisable()
		{
			RobotRampagePopupEvents.OnOpenVictoryPopup -= OpenVictoryPopup;
			RobotRampagePopupEvents.OnOpenDefeatPopup -= OpenDefeatPopup;
		}

		private void OpenVictoryPopup()
		{
			_victoryPopup.Activate();
		}

		private void OpenDefeatPopup()
		{
			_defeatPopup.Activate();
		}
	}
}