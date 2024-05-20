using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageWaveController: MonoBehaviour
    {
        [SerializeField]
        private float currentWaveTimer = 30f;

        [SerializeField]
        private GameObject _enemyToSpawn;

        private int _minAmountToKeepOnScreen = 10;
        private int _maxAmountToSpawn = 20;
        
        [SerializeField]
        private float _timeToStart = 1f;
        
        [SerializeField]
        private bool _started = false;

        private void Update()
        {
            if (!_started)
            {
                _timeToStart -= Time.deltaTime;
                if (_timeToStart <= 0)
                {
                    _started = true;
                    RobotRampageTimerEvents.RaiseStartTimerEvent(currentWaveTimer);
                    RobotRampageWaveEvents.RaiseStartWaveSpawnEvent(_enemyToSpawn, _minAmountToKeepOnScreen, _maxAmountToSpawn);
                }
            }
        }
    }
}