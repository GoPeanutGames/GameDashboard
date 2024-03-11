using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI.EndGame
{
	[RequireComponent(typeof(Button))]
	public class BattleDashHomeButton: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _audioClip;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;
		

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnHomeButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnHomeButtonClick);
		}

		private void OnHomeButtonClick()
		{
			BattleDashClientUIEvents.RaiseOpenEndGamePopupEvent();
			StartCoroutine(PlaySfx());
		}

		private IEnumerator PlaySfx()
		{
			yield return new WaitForSecondsRealtime(0.3f);
			BattleDashClientAudioEvents.RaisePlaySfxEvent(_audioClip, 1);
		}
	}
}