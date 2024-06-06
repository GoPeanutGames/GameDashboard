using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(PassiveUpgrade) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(PassiveUpgrade)
	)]
	public class PassiveUpgrade: BaseUpgrade
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private UpgradeLevel _upgradeLevel;
		
		public UpgradeLevel UpgradeLevel => _upgradeLevel;
	}
}