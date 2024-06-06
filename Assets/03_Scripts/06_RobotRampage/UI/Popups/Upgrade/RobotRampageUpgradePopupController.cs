using System;
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
				robotRampageUpgradeChoice.AddListener(OnUpgradeChosen);
			}
		}

		private void OnDestroy()
		{
			foreach (RobotRampageUpgradeChoice robotRampageUpgradeChoice in _upgradeChoices){
				if (robotRampageUpgradeChoice != null){
					robotRampageUpgradeChoice.RemoveListener(OnUpgradeChosen);
				}
			}
		}

		private void OnTriggerUpgradeUI()
		{
			List<BaseUpgrade> tempList = new List<BaseUpgrade>(_upgradePool);
			for (int i = 0; i < 3; i++){
				int randomIndex = Random.Range(0, tempList.Count);
				_upgradeChoices[i].SetupChoice(tempList[randomIndex]);
				tempList.RemoveAt(randomIndex);
			}
			//TODO: character has current upgrades
			//TODO: upgrade is a base class
			//TODO: extended by passive & weapon
			//TODO: there is a "pool" of current upgrades that's set on start
			//TODO: when the character starts, it will trigger its own "upgrade" that will add the weapon
			//TODO:		- this will either take the weapon from the pool if it's there and add the next upgrade
			//TODO:		- or if it's a special weapon available for this char, it won't be in the pool, which means this will just add the upgrade to the pool
			//TODO; when an upgrade is chosen, it is removed from the pool, and the upgrade is asked to provide next update that is then added to the pool
			//TODO: UI handler that will handle sprites depending on upgrade
			//TODO: player upgrade controller that will handle passive / weapon depending on upgrade. E.g. update stats, or add weapon, or update weapon stats
		}

		private void OnUpgradeChosen(BaseUpgrade baseUpgrade)
		{
			Debug.Log($"CHOSEN: {baseUpgrade.BaseUpgradeType}");
			_upgradePool.Remove(baseUpgrade);
			if (baseUpgrade.NextUpgrade != null)
			{
				_upgradePool.Add(baseUpgrade.NextUpgrade);
			}
			//TODO: send event to apply upgrade to character
			RobotRampageUpgradeEvents.RaiseOnUpgradeChosenEvent();
		}
	}
}