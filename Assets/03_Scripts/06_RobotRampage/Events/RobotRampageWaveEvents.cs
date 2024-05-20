using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
    public static class RobotRampageWaveEvents
    {
        private static UnityAction<GameObject, int, int> _startWaveSpawn;
		
        public static event UnityAction<GameObject, int, int> OnStartWaveSpawn
        {
            add => _startWaveSpawn += value;
            remove => _startWaveSpawn -= value;
        }
		
        public static void RaiseStartWaveSpawnEvent(GameObject prefab, int min, int max)
        {
            if (_startWaveSpawn == null){
                LoggerService.LogWarning($"{nameof(RobotRampageWaveEvents)}::{nameof(RaiseStartWaveSpawnEvent)} raised, but nothing picked it up");
                return;
            }
            _startWaveSpawn.Invoke(prefab, min, max);
        }
    }
}