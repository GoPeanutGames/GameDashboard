using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(RobotRampageExpCollection) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageConfigs + nameof(RobotRampageExpCollection)
	)]
	public class RobotRampageExpCollection: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageExpData _lowestExpData;
		
		[SerializeField]
		private RobotRampageExpData _lowExpData;

		public RobotRampageExpData GetExperienceData(RobotRampageExpType expType)
		{
			switch (expType){
				case RobotRampageExpType.Lowest:
					return _lowestExpData;
				case RobotRampageExpType.Low:
					return _lowExpData;
			}
			return null;
		}
	}
}