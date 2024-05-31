using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageHomeController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageCharacterData _chosenCharacter;

		private void Start()
		{
			RobotRampageCharacterStatsService.SetCharacter(_chosenCharacter);
		}
	}
}