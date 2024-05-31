using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(RobotRampageExpData) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(RobotRampageExpData)
	)]
	public class RobotRampageExpData: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Sprite _expImage;

		[SerializeField]
		private float _expAmount;

		public Sprite ExpImage => _expImage;

		public float ExpAmount => _expAmount;
	}
}