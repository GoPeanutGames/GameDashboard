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

		[SerializeField]
		private Sprite _icon;

		[SerializeField]
		private Sprite _levelIcon;

		[SerializeField]
		private string _title;

		[SerializeField]
		private string _description;

		[SerializeField]
		private BaseUpgrade _nextUpgrade;
		
		public BaseUpgradeType BaseUpgradeType => _baseUpgradeType;

		public Sprite Background => _background;

		public Sprite Icon => _icon;

		public Sprite LevelIcon => _levelIcon;

		public string Title => _title;

		public string Description => _description;
		
		public BaseUpgrade NextUpgrade => _nextUpgrade;
	}
}