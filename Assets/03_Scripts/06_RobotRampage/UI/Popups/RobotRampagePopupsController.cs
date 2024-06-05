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
		
		[SerializeField]
		private GameObject _upgradesPopup;

		private void OnEnable()
		{
			RobotRampagePopupEvents.OnOpenVictoryPopup += OpenVictoryPopup;
			RobotRampagePopupEvents.OnOpenDefeatPopup += OpenDefeatPopup;
			RobotRampageUpgradeEvents.OnTriggerUpgradesUI += OpenUpgradesPopup;
			RobotRampageUpgradeEvents.OnUpgradeChosen += CloseUpgradePopup;
		}

		private void OnDisable()
		{
			RobotRampagePopupEvents.OnOpenVictoryPopup -= OpenVictoryPopup;
			RobotRampagePopupEvents.OnOpenDefeatPopup -= OpenDefeatPopup;
			RobotRampageUpgradeEvents.OnTriggerUpgradesUI -= OpenUpgradesPopup;
			RobotRampageUpgradeEvents.OnUpgradeChosen -= CloseUpgradePopup;
		}

		private void OpenVictoryPopup()
		{
			_victoryPopup.Activate();
		}

		private void OpenDefeatPopup()
		{
			_defeatPopup.Activate();
		}

		private void OpenUpgradesPopup()
		{
			_upgradesPopup.Activate();
		}

		private void CloseUpgradePopup()
		{
			_upgradesPopup.Deactivate();
		}
	}
}