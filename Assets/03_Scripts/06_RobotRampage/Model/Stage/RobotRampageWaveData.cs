using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    [Serializable]
    public class RobotRampageWaveData
    {
        public float waveTimer;
        public List<RobotRampageWaveMonsterData> robotRampageWaveMonsterData;
    }

    [Serializable]
    public class RobotRampageWaveMonsterData
    {
        public GameObject prefab;
        public int minToSpawn;
        public int maxToSpawn;
    }
}