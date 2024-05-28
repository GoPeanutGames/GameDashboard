using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageStageAudioController: MonoBehaviour
	{
		private void Start()
		{
			RobotRampageAudioEvents.RaisePlayBgMusicEvent(RobotRampageStageService.currentStageData.StageBackgroundMusic, true);
		}
	}
}