using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
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

		private void OnEnable()
		{
			BattleDashAudioEvents.OnPlayMusic += OnPlayMusic;
			BattleDashAudioEvents.OnPlaySfx += OnPlaySfx;
			BattleDashAudioEvents.OnTriggerMuteUnMute += OnTriggerMuteUnMute;
		}

		private void OnDisable()
		{
			BattleDashAudioEvents.OnPlayMusic -= OnPlayMusic;
			BattleDashAudioEvents.OnPlaySfx -= OnPlaySfx;
			BattleDashAudioEvents.OnTriggerMuteUnMute -= OnTriggerMuteUnMute;
		}

		private void OnTriggerMuteUnMute()
		{
			muted = !muted;
			_musicSource.mute = muted;
			_sfxSource.mute = muted;
		}

		private void OnPlayMusic(AudioClip audioClip)
		{
			_musicSource.loop = true;
			_musicSource.clip = audioClip;
			_musicSource.volume = 0;
			_musicSource.Play();
			FadeInMusic();
		}

		private void OnPlaySfx(AudioClip audioClip, float volume)
		{
			_sfxSource.PlayOneShot(audioClip, volume);
		}
		
		private void FadeInMusic() {
			StopAllCoroutines();
			StartCoroutine(StartFade(_musicSource, 1, 1f));
		}

		private void FadeOutMusic() {
			StopAllCoroutines();
			StartCoroutine(StartFade(_musicSource, 1, 0f));
		}

		private static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
			float currentTime = 0;
			float start = audioSource.volume;
			while (currentTime < duration) {
				currentTime += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
				yield return null;
			}
			yield break;
		}
	}
}