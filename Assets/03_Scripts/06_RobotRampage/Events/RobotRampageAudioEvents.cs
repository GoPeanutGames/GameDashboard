using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageAudioEvents
	{
		private static UnityAction<AudioClip, bool> _playBgMusic;
		
		private static UnityAction<AudioClip> _playSfxOneShot;
		
		public static event UnityAction<AudioClip, bool> OnPlayBgMusic
		{
			add => _playBgMusic += value;
			remove => _playBgMusic -= value;
		}
		
		public static event UnityAction<AudioClip> OnPlaySfxOneShot
		{
			add => _playSfxOneShot += value;
			remove => _playSfxOneShot -= value;
		}
		
		public static void RaisePlayBgMusicEvent(AudioClip clip, bool loop)
		{
			if (_playBgMusic == null){
				LoggerService.LogWarning($"{nameof(RobotRampageAudioEvents)}::{nameof(RaisePlayBgMusicEvent)} raised, but nothing picked it up");
				return;
			}
			_playBgMusic.Invoke(clip, loop);
		}
		
		public static void RaisePlaySfxOneShotEvent(AudioClip clip)
		{
			if (_playSfxOneShot == null){
				LoggerService.LogWarning($"{nameof(RobotRampageAudioEvents)}::{nameof(RaisePlaySfxOneShotEvent)} raised, but nothing picked it up");
				return;
			}
			_playSfxOneShot.Invoke(clip);
		}
	}
}