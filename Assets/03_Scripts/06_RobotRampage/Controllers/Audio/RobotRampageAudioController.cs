using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageAudioController: MonoSingleton<RobotRampageAudioController>
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioSource _bgAudioSource;
		
		private void OnEnable()
		{
			RobotRampageAudioEvents.OnPlayBgMusic += OnPlayBgMusic;
		}

		private void OnDisable()
		{
			RobotRampageAudioEvents.OnPlayBgMusic -= OnPlayBgMusic;
		}

		private void OnPlayBgMusic(AudioClip clip, bool loop)
		{
			_bgAudioSource.clip = clip;
			_bgAudioSource.loop = loop;
			_bgAudioSource.Play();
		}
	}
}