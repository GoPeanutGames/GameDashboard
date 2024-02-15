using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashAudioEvents
	{
		private static UnityAction _triggerMuteUnMute;
		private static UnityAction<AudioClip> _playMusic;
		private static UnityAction<AudioClip, float> _playSfx;

		public static event UnityAction<AudioClip> OnPlayMusic
		{
			add => _playMusic += value;
			remove => _playMusic -= value;
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

		public static void RaisePlayMusicEvent(AudioClip music)
		{
			if (_playMusic == null){
				LoggerService.LogWarning($"{nameof(BattleDashAudioEvents)}::{nameof(RaisePlayMusicEvent)} raised, but nothing picked it up");
				return;
			}
			_playMusic.Invoke(music);
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