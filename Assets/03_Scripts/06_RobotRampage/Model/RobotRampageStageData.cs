using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    [CreateAssetMenu(
        fileName = nameof(RobotRampageStageData) + ExtensionNames.DotAsset,
        menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(RobotRampageStageData)
    )]
    public class RobotRampageStageData: ScriptableObject
    {
        [SerializeField]
        private string _stageName;
        
        [SerializeField]
        private Sprite _defaultBackground;

        [SerializeField]
        private List<Sprite> _possibleDecor;

        [SerializeField]
        private List<RobotRampageWaveData> _wavesData;

        [SerializeField]
        private AudioClip _stageBackgroundMusic;

        public string StageName => _stageName;
        
        public Sprite DefaultBackground => _defaultBackground;

        public List<Sprite> PossibleDecor => _possibleDecor;

        public List<RobotRampageWaveData> WavesData => _wavesData;

        public AudioClip StageBackgroundMusic => _stageBackgroundMusic;
    }
}