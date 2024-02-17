using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class ClientBattleDashPlayerSfx: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _sfxClip;
		
		[SerializeField]
		private AudioClip _deathClip;
		
		public void PlayShootSfx()
		{
			ClientBattleDashAudioEvents.RaisePlaySfxEvent(_sfxClip, 0.7f);
		}
		
		public void PlayDeathSfx()
		{
			ClientBattleDashAudioEvents.RaisePlaySfxEvent(_deathClip, 1);
		}
	}
}