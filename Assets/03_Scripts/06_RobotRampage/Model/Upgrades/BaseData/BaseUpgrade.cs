using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class BaseUpgrade: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private BaseUpgradeType _baseUpgradeType;

		[SerializeField]
		private Sprite _background;

		public BaseUpgradeType BaseUpgradeType => _baseUpgradeType;

		public Sprite Background => _background;
	}
}