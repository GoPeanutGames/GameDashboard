﻿using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Audio
{
	public class BattleDashAudioController: MonoBehaviour
	{
		public static bool muted;
		
		[SerializeField]
		private AudioSource _musicSource;
		
		[SerializeField]
		private AudioSource _sfxSource;
		
#if !SERVER

		private void OnEnable()
		{
			BattleDashClientAudioEvents.OnFadeInMusic += OnFadeInMusic;
			BattleDashClientAudioEvents.OnFadeOutMusic += OnFadeOutMusic;
			BattleDashClientAudioEvents.OnPlaySfx += OnPlaySfx;
			BattleDashClientAudioEvents.OnTriggerMuteUnMute += OnTriggerMuteUnMute;
		}

		private void OnDisable()
		{
			BattleDashClientAudioEvents.OnFadeInMusic -= OnFadeInMusic;
			BattleDashClientAudioEvents.OnFadeOutMusic -= OnFadeOutMusic;
			BattleDashClientAudioEvents.OnPlaySfx -= OnPlaySfx;
			BattleDashClientAudioEvents.OnTriggerMuteUnMute -= OnTriggerMuteUnMute;
		}

		private void OnTriggerMuteUnMute()
		{
			muted = !muted;
			_musicSource.mute = muted;
			_sfxSource.mute = muted;
		}

		private void OnFadeInMusic(AudioClip audioClip)
		{
			if (audioClip != _musicSource.clip){
				_musicSource.loop = true;
				_musicSource.clip = audioClip;
				_musicSource.volume = 0;
				_musicSource.Play();
				FadeInMusic();
			}
			else{
				FadeInMusic();
			}
		}

		private void OnFadeOutMusic(float duration)
		{
			FadeOutMusic(duration);
		}

		private void OnPlaySfx(AudioClip audioClip, float volume)
		{
			LoggerService.LogInfo($"{nameof(BattleDashAudioController)}::{nameof(OnPlaySfx)} - {audioClip.name}");
			_sfxSource.PlayOneShot(audioClip, volume);
		}
		
		private void FadeInMusic() {
			StopAllCoroutines();
			StartCoroutine(StartFade(_musicSource, 1, 1f));
		}

		private void FadeOutMusic(float duration) {
			StopAllCoroutines();
			StartCoroutine(StartFade(_musicSource, duration, 0f));
		}

		private static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
			float currentTime = 0;
			float start = audioSource.volume;
			while (currentTime < duration) {
				currentTime += Time.unscaledDeltaTime;
				audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
				yield return null;
			}
			yield break;
		}
#endif
	}
}