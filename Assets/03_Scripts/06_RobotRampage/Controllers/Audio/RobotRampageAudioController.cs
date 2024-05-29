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
		
		[SerializeField]
		private AudioSource _sfxSource;
		
		private void OnEnable()
		{
			RobotRampageAudioEvents.OnPlayBgMusic += OnPlayBgMusic;
			RobotRampageAudioEvents.OnPlaySfxOneShot += OnPlaySfxOneShot;
		}

		private void OnDisable()
		{
			RobotRampageAudioEvents.OnPlayBgMusic -= OnPlayBgMusic;
			RobotRampageAudioEvents.OnPlaySfxOneShot -= OnPlaySfxOneShot;
		}

		private void OnPlayBgMusic(AudioClip clip, bool loop)
		{
			_bgAudioSource.clip = clip;
			_bgAudioSource.loop = loop;
			_bgAudioSource.Play();
		}

		private void OnPlaySfxOneShot(AudioClip clip)
		{
			_sfxSource.PlayOneShot(clip);
		}
	}
}