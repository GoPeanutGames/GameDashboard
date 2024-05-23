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
        private Sprite _defaultBackground;

        public Sprite DefaultBackground => _defaultBackground;
    }
}