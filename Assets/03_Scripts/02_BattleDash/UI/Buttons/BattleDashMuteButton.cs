using PeanutDashboard._02_BattleDash.Audio;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI.Buttons
{
	[RequireComponent(typeof(Button))]
	public class BattleDashMuteButton: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Sprite _muteSprite;
		
		[SerializeField]
		private Sprite _unMuteSprite;
		
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;

		[SerializeField]
		private Image _image;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_image = GetComponent<Image>();
			_image.sprite = BattleDashAudioController.muted ? _muteSprite : _unMuteSprite;
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnMuteButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnMuteButtonClick);
		}

		private void OnMuteButtonClick()
		{
			BattleDashAudioEvents.RaiseTriggerMuteUnMuteEvent();
			_image.sprite = BattleDashAudioController.muted ? _muteSprite : _unMuteSprite;
		}
	}
}