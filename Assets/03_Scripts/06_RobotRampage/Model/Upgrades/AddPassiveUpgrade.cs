using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(AddPassiveUpgrade) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(AddPassiveUpgrade)
	)]
	public class AddPassiveUpgrade: BaseUpgrade
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