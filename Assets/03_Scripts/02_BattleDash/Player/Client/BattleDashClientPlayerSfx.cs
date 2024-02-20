using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashClientPlayerSfx: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _sfxClip;
		
		[SerializeField]
		private AudioClip _deathClip;
		
		public void PlayShootSfx()
		{
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_sfxClip, 0.7f);
		}
		
		public void PlayDeathSfx()
		{
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_deathClip, 1);
		}
	}
}