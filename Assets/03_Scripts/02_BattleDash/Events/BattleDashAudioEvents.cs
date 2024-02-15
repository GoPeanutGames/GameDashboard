using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashAudioEvents
	{
		private static UnityAction _triggerMuteUnMute;
		private static UnityAction<AudioClip> _fadeInMusic;
		private static UnityAction<AudioClip> _fadeOutMusic;
		private static UnityAction<AudioClip, float> _playSfx;

		public static event UnityAction<AudioClip> OnFadeInMusic
		{
			add => _fadeInMusic += value;
			remove => _fadeInMusic -= value;
		}
		
		public static event UnityAction<AudioClip> OnFadeOutMusic
		{
			add => _fadeOutMusic += value;
			remove => _fadeOutMusic -= value;
		}

		public static event UnityAction<AudioClip, float> OnPlaySfx
		{
			add => _playSfx += value;
			remove => _playSfx -= value;
		}

		public static event UnityAction OnTriggerMuteUnMute
		{
			add => _triggerMuteUnMute += value;
			remove => _triggerMuteUnMute -= value;
		}

		public static void RaiseTriggerMuteUnMuteEvent()
		{
			if (_triggerMuteUnMute == null){
				LoggerService.LogWarning($"{nameof(BattleDashAudioEvents)}::{nameof(RaiseTriggerMuteUnMuteEvent)} raised, but nothing picked it up");
				return;
			}
			_triggerMuteUnMute.Invoke();
		}

		public static void RaiseFadeInMusicEvent(AudioClip music)
		{
			if (_fadeInMusic == null){
				LoggerService.LogWarning($"{nameof(BattleDashAudioEvents)}::{nameof(RaiseFadeInMusicEvent)} raised, but nothing picked it up");
				return;
			}
			_fadeInMusic.Invoke(music);
		}

		public static void RaiseFadeOutMusicEvent(AudioClip music)
		{
			if (_fadeOutMusic == null){
				LoggerService.LogWarning($"{nameof(BattleDashAudioEvents)}::{nameof(RaiseFadeOutMusicEvent)} raised, but nothing picked it up");
				return;
			}
			_fadeOutMusic.Invoke(music);
		}

		public static void RaisePlaySfxEvent(AudioClip sfx, float volume)
		{
			if (_playSfx == null){
				LoggerService.LogWarning($"{nameof(BattleDashAudioEvents)}::{nameof(RaisePlaySfxEvent)} raised, but nothing picked it up");
				return;
			}
			_playSfx.Invoke(sfx, volume);
		}
	}
}