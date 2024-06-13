using System.Collections.Generic;
using PeanutDashboard._06_RobotRampage.Collection;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageUpgradePopupController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<RobotRampageUpgradeChoice> _upgradeChoices;

		[SerializeField]
		private List<BaseUpgrade> _upgradePool;

		[SerializeField]
		private UpgradeCollection _startingUpgrades;

		private void OnEnable()
		{
			RobotRampageUpgradeEvents.OnTriggerUpgradesUI += OnTriggerUpgradeUI;
		}

		private void OnDisable()
		{
			RobotRampageUpgradeEvents.OnTriggerUpgradesUI -= OnTriggerUpgradeUI;
		}

		private void Start()
		{
			_upgradePool = new List<BaseUpgrade>(_startingUpgrades.GetUpgrades());
			foreach (RobotRampageUpgradeChoice robotRampageUpgradeChoice in _upgradeChoices){
				robotRampageUpgradeChoice.AddListener(OnChoiceClick);
			}
			foreach (BaseUpgrade startingUpgrade in RobotRampageCharacterStatsService.GetStartingUpgrades()){
				OnUpgradeChosen(startingUpgrade, true);
			}
		}

		private void OnDestroy()
		{
			foreach (RobotRampageUpgradeChoice robotRampageUpgradeChoice in _upgradeChoices){
				if (robotRampageUpgradeChoice != null){
					robotRampageUpgradeChoice.RemoveListener(OnChoiceClick);
				}
			}
		}

		private void OnTriggerUpgradeUI()
		{
			RobotRampagePauseEvents.RaisePauseGameEventEvent();
			List<BaseUpgrade> tempList = new List<BaseUpgrade>(_upgradePool);
			for (int i = 0; i < 3; i++){
				if (tempList.Count > 0){
					int randomIndex = Random.Range(0, tempList.Count);
					_upgradeChoices[i].SetupChoice(tempList[randomIndex]);
					tempList.RemoveAt(randomIndex);
				}
				else{
					_upgradeChoices[i].SetupUnavailable();
				}
			}
		}

		private void OnChoiceClick(BaseUpgrade baseUpgrade)
		{
			OnUpgradeChosen(baseUpgrade, false);
		}

		private void OnUpgradeChosen(BaseUpgrade baseUpgrade, bool starting)
		{
			Debug.Log($"{nameof(RobotRampageUpgradePopupController)}::{nameof(OnUpgradeChosen)} - {baseUpgrade.BaseUpgradeType}");
			_upgradePool.Remove(baseUpgrade);
			if (baseUpgrade.NextUpgrade != null)
			{
				_upgradePool.Add(baseUpgrade.NextUpgrade);
			}
			RobotRampageUpgradeEvents.RaiseApplyUpgradeEvent(baseUpgrade);
			if (!starting){
				RobotRampageUpgradeEvents.RaiseOnUpgradeChosenEvent();
				RobotRampagePauseEvents.RaiseUnPauseGameEventEvent();
			}
		}
	}
}