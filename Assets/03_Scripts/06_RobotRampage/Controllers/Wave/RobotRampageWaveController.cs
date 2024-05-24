using System;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageWaveController: MonoBehaviour
    {
        [SerializeField]
        private int _currentWaveIndex = 0;
        
        [SerializeField]
        private float _timeToStart = 1f;
        
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

        private void Update()
        {
            if (!_started)
            {
                _timeToStart -= Time.deltaTime;
                if (_timeToStart <= 0)
                {
                    _started = true;
                    TriggerNextWave();
                }
            }
        }

        private void OnTimerDone()
        {
            _currentWaveIndex++;
            if (_currentWaveIndex < RobotRampageStageService.currentStageData.WavesData.Count)
            {
                TriggerNextWave();
            }
            else
            {
                //TODO: victory?
            }
        }

        private void TriggerNextWave()
        {
            RobotRampageTimerEvents.RaiseStartTimerEvent(RobotRampageStageService.currentStageData.WavesData[_currentWaveIndex].waveTimer);
            RobotRampageWaveEvents.RaiseStartWaveSpawnEvent(RobotRampageStageService.currentStageData.WavesData[_currentWaveIndex].robotRampageWaveMonsterData);
        }
    }
}