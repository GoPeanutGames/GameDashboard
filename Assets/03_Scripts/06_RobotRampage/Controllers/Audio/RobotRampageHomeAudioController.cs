using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageHomeAudioController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _homeBgMusic;

		private void Start()
		{
			RobotRampageAudioEvents.RaisePlayBgMusicEvent(_homeBgMusic, true);
		}
	}
}