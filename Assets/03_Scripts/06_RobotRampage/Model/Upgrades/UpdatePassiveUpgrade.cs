using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(UpdatePassiveUpgrade) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(UpdatePassiveUpgrade)
	)]
	public class UpdatePassiveUpgrade: BaseUpgrade
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private UpgradeLevel _upgradeLevel;

		[SerializeField]
		private PassiveType _passiveType;
		
		public UpgradeLevel UpgradeLevel => _upgradeLevel;

		public PassiveType PassiveType => _passiveType;
	}
}