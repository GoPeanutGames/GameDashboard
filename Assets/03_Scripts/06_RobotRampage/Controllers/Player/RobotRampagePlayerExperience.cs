using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerExperience: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private CircleCollider2D _circleCollider2D;

		[SerializeField]
		private int _currentLevel = 0;

		[SerializeField]
		private float _currentExp;
		
		[SerializeField]
		private float _expToNextLevel;
		
		private void Awake()
		{
			_circleCollider2D = GetComponent<CircleCollider2D>();
			_circleCollider2D.radius = RobotRampageCharacterStatsService.GetAttractionRange();
			_expToNextLevel = RobotRampageCharacterStatsService.GetExpToNextLevel(_currentLevel);
		}

		private void OnEnable()
		{
			RobotRampagePlayerEvents.OnAddPlayerExperience += OnAddPlayerExperience;
			RobotRampageUpgradeEvents.OnUpgradeChosen += OnUpgradeChosen;
			RobotRampageUpgradeEvents.OnRefreshStats += OnRefreshStats;
		}

		private void OnDisable()
		{
			RobotRampagePlayerEvents.OnAddPlayerExperience -= OnAddPlayerExperience;
			RobotRampageUpgradeEvents.OnUpgradeChosen -= OnUpgradeChosen;
			RobotRampageUpgradeEvents.OnRefreshStats -= OnRefreshStats;
		}

		private void OnRefreshStats()
		{
			_circleCollider2D.radius = RobotRampageCharacterStatsService.GetAttractionRange();
		}

		private void OnAddPlayerExperience(float exp)
		{
			_currentExp += exp;
			if (_currentExp >= _expToNextLevel){
				RobotRampageLevelUIEvents.RaiseUpdateUIExpEvent(_expToNextLevel, _expToNextLevel);
				RobotRampageUpgradeEvents.RaiseTriggerUpgradesUIEvent();
			}
			else{
				RobotRampageLevelUIEvents.RaiseUpdateUIExpEvent(_currentExp, _expToNextLevel);
			}
		}

		private void OnUpgradeChosen()
		{
			_currentExp -= _expToNextLevel;
			_currentLevel += 1;
			_expToNextLevel = RobotRampageCharacterStatsService.GetExpToNextLevel(_currentLevel);
			RobotRampageLevelUIEvents.RaiseUpdateUIExpEvent(_currentExp, _expToNextLevel);
			RobotRampageLevelUIEvents.RaiseUpdateUILevelEvent(_currentLevel + 1);
			//TODO: add chosen weapon / passive + update UI
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			RobotRampageExpController expController = other.GetComponent<RobotRampageExpController>();
			if (expController != null){
				expController.StartAttraction();
			}
		}
	}
}