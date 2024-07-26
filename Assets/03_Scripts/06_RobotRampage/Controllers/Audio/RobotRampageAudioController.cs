using System;
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
			RobotRampageAudioEvents.OnTriggerMute += OnTriggerMute;
		}

		private void OnDisable()
		{
			RobotRampageAudioEvents.OnPlayBgMusic -= OnPlayBgMusic;
			RobotRampageAudioEvents.OnPlaySfxOneShot -= OnPlaySfxOneShot;
			RobotRampageAudioEvents.OnTriggerMute -= OnTriggerMute;
		}

		private void Start()
		{
			_bgAudioSource.mute = AudioService.bgMusicMuted;
			_sfxSource.mute = AudioService.sfxMuted;
		}

		private void OnTriggerMute(bool mute)
		{
			AudioService.bgMusicMuted = mute;
			AudioService.sfxMuted = mute;
			_bgAudioSource.mute = mute;
			_sfxSource.mute = mute;
		}

		private void OnPlayBgMusic(AudioClip clip, bool loop)
		{
			_bgAudioSource.clip = clip;
			_bgAudioSource.loop = loop;
			_bgAudioSource.Play();
		}

		private void OnPlaySfxOneShot(AudioClip clip, float volume)
		{
			_sfxSource.PlayOneShot(clip, volume);
		}
	}
}