using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.Win
{
	public class BattleDashWinUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _wonUI;
		
		[SerializeField]
		private AudioClip _audioClip;
#if !SERVER
		private void OnEnable()
		{
			BattleDashClientUIEvents.OnShowWon += OnShowGameOver;
		}

		private void OnDisable()
		{
			BattleDashClientUIEvents.OnShowWon -= OnShowGameOver;
		}

		private void OnShowGameOver()
		{
			BattleDashClientAudioEvents.RaiseFadeOutMusicEvent(3f);
			StartCoroutine(ShowWonUI());
		}

		private IEnumerator ShowWonUI()
		{
			yield return new WaitForSecondsRealtime(3.1f);
			_wonUI.Activate();
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_audioClip,1);
		}
#endif
	}
}