using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageWaveController : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _timeToStart;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _currentWaveIndex = 0;

		[SerializeField]
		private bool _started = false;

		private void OnEnable()
		{
			RobotRampageTimerEvents.OnTimerDone += OnTimerDone;
		}

		private void OnDisable()
		{
			RobotRampageTimerEvents.OnTimerDone -= OnTimerDone;
		}

		private void Start()
		{
			RobotRampageUIEvents.RaiseShowCentralNotificationEvent(RobotRampageStageService.currentStageData.StageName);
		}

		private void Update()
		{
			if (!_started){
				_timeToStart -= Time.deltaTime;
				if (_timeToStart <= 0){
					_started = true;
					TriggerNextWave();
				}
			}
		}

		private void OnTimerDone()
		{
			_currentWaveIndex++;
			if (_currentWaveIndex < RobotRampageStageService.currentStageData.WavesData.Count){
				TriggerNextWave();
			}
			else{
				RobotRampagePopupEvents.RaiseOpenVictoryPopupEvent();
			}
		}

		private void TriggerNextWave()
		{
			RobotRampageUIEvents.RaiseShowCentralNotificationEvent($"Wave {_currentWaveIndex + 1}");
			RobotRampageTimerEvents.RaiseStartTimerEvent(RobotRampageStageService.currentStageData.WavesData[_currentWaveIndex].waveTimer);
			RobotRampageWaveEvents.RaiseStartWaveSpawnEvent(RobotRampageStageService.currentStageData.WavesData[_currentWaveIndex].robotRampageWaveMonsterData);
		}
	}
}