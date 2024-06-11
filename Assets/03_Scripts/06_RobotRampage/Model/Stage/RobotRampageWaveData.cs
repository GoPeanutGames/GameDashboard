using System;
using System.Collections.Generic;

namespace PeanutDashboard._06_RobotRampage
{
    /// <summary>
    /// This is a general wave holder
    /// Information:
    ///     - Wave length in time
    ///     - Monster spawn data
    /// </summary>
    [Serializable]
    public class RobotRampageWaveData
    {
        public float waveTimer;
        public List<RobotRampageSubWaveTrigger> robotRampageWaveMonsterData;
    }

    /// <summary>
    /// This holds information about when to start this monster spawn configuration
    /// Information:
    ///     - Time when to start
    ///     - List of monsters with min/max spawns
    /// </summary>
    [Serializable]
    public class RobotRampageSubWaveTrigger
    {
        public float timeToStart;
        public bool keepLast;
        public List<RobotRampageMonsterSpawnConfig> spawnConfig;
    }
    
    /// <summary>
    /// Monster spawn configuration
    /// Information
    ///     - Which monster
    ///     - Min and Max amounts
    /// </summary>
    [Serializable]
    public class RobotRampageMonsterSpawnConfig
    {
        public MobType mobType;
        public int minToSpawn;
        public int maxToSpawn;
    }
}