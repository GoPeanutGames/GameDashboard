using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
    public static class RobotRampageWaveEvents
    {
        private static UnityAction<List<RobotRampageSubWaveTrigger>> _startWaveSpawn;
		
        public static event UnityAction<List<RobotRampageSubWaveTrigger>> OnStartWaveSpawn
        {
            add => _startWaveSpawn += value;
            remove => _startWaveSpawn -= value;
        }
		
        public static void RaiseStartWaveSpawnEvent(List<RobotRampageSubWaveTrigger> robotRampageMonstersData)
        {
            if (_startWaveSpawn == null){
                LoggerService.LogWarning($"{nameof(RobotRampageWaveEvents)}::{nameof(RaiseStartWaveSpawnEvent)} raised, but nothing picked it up");
                return;
            }
            _startWaveSpawn.Invoke(robotRampageMonstersData);
        }
    }
}