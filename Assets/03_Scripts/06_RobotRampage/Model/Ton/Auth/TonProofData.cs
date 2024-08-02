using System;

namespace PeanutDashboard._06_RobotRampage
{
    [Serializable]
    public class TonProofData
    {
        public TonProofDomain domain;
        public string payload;
        public string signature;
        public uint timestamp;
    }
}