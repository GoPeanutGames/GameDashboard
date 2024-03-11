using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.GameOver
{
	public class BattleDashGameOverUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _gameOverUI;

		[SerializeField]
		private AudioClip _audioClip;

#if !SERVER
		private void OnEnable()
		{
			BattleDashClientUIEvents.OnShowGameOver += OnShowGameOver;
		}

		private void OnDisable()
		{
			BattleDashClientUIEvents.OnShowGameOver -= OnShowGameOver;
		}

		private void OnShowGameOver()
		{
			_gameOverUI.Activate();
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_audioClip,1);
		}
#endif
	}
}