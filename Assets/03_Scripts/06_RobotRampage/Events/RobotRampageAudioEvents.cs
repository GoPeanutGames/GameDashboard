using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageAudioEvents
	{
		private static UnityAction<AudioClip, bool> _playBgMusic;
		
		public static event UnityAction<AudioClip, bool> OnPlayBgMusic
		{
			add => _playBgMusic += value;
			remove => _playBgMusic -= value;
		}
		
		public static void RaisePlayBgMusicEvent(AudioClip clip, bool loop)
		{
			if (_playBgMusic == null){
				LoggerService.LogWarning($"{nameof(RobotRampageAudioEvents)}::{nameof(RaisePlayBgMusicEvent)} raised, but nothing picked it up");
				return;
			}
			_playBgMusic.Invoke(clip, loop);
		}
	}
}