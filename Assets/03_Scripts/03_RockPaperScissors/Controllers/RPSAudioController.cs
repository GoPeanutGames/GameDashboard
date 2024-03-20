using System.Collections;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSAudioController: MonoBehaviour
	{
		public static bool muted;
		
		[SerializeField]
		private AudioSource _musicSource;
		
		[SerializeField]
		private AudioSource _sfxSource;
		
		private void OnEnable()
		{
			RPSAudioEvents.OnFadeInMusic += OnFadeInMusic;
			RPSAudioEvents.OnFadeOutMusic += OnFadeOutMusic;
			RPSAudioEvents.OnPlaySfx += OnPlaySfx;
			RPSAudioEvents.OnTriggerMuteUnMute += OnTriggerMuteUnMute;
		}

		private void OnDisable()
		{
			RPSAudioEvents.OnFadeInMusic -= OnFadeInMusic;
			RPSAudioEvents.OnFadeOutMusic -= OnFadeOutMusic;
			RPSAudioEvents.OnPlaySfx -= OnPlaySfx;
			RPSAudioEvents.OnTriggerMuteUnMute -= OnTriggerMuteUnMute;
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
		}

		private void OnFadeOutMusic(float duration)
		{
			FadeOutMusic(duration);
		}

		private void OnPlaySfx(AudioClip audioClip, float volume)
		{
			LoggerService.LogInfo($"{nameof(RPSAudioController)}::{nameof(OnPlaySfx)} - {audioClip.name}");
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
	}
}