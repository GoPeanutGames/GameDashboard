using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePauseController: MonoBehaviour
	{
		private void OnEnable()
		{
			RobotRampagePauseEvents.OnPauseGame += OnPauseGame;
			RobotRampagePauseEvents.OnUnPauseGame += OnUnPauseGame;
		}

		private void OnDisable()
		{
			RobotRampagePauseEvents.OnPauseGame -= OnPauseGame;
			RobotRampagePauseEvents.OnUnPauseGame -= OnUnPauseGame;
		}

		private void OnPauseGame()
		{
			Time.timeScale = 0;
		}

		private void OnUnPauseGame()
		{
			Time.timeScale = 1;			
		}
	}
}