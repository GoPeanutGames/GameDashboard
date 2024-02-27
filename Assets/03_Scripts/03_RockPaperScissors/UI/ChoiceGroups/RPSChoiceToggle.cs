using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSChoiceToggle: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private bool _selectedAtStart;
		
		[SerializeField]
		private Toggle _toggle;
		
		[SerializeField]
		private TMP_Text _text;

		[SerializeField]
		private Sprite _selectedSprite;
		
		[SerializeField]
		private Sprite _deSelectedSprite;
		
		[SerializeField]
		private TMP_FontAsset _selectedTextFont;
		
		[SerializeField]
		private TMP_FontAsset _normalTextFont;

		private void Awake()
		{
			if (_selectedAtStart){
				_toggle.SetIsOnWithoutNotify(true);
				OnToggleValueChanged(true);
			}
		}

		private void OnEnable()
		{
			_toggle.onValueChanged.AddListener(OnToggleValueChanged);
			RPSClientGameEvents.OnDisablePlayerChoices += OnDisableToggle;
			RPSClientGameEvents.OnEnablePlayerChoices += OnEnableToggle;
		}

		private void OnDisable()
		{
			_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
			RPSClientGameEvents.OnDisablePlayerChoices -= OnDisableToggle;
			RPSClientGameEvents.OnEnablePlayerChoices -= OnEnableToggle;
		}

		private void OnEnableToggle()
		{
			_toggle.interactable = true;
			_text.color = new Color(0, 0, 0, 1);
		}
		
		private void OnDisableToggle()
		{
			_toggle.interactable = false;
			_text.color = new Color(0,0,0,0.5f);
		}

		private void OnToggleValueChanged(bool value)
		{
			_toggle.image.sprite = value ? _selectedSprite : _deSelectedSprite;
			_text.font = value ? _selectedTextFont : _normalTextFont;
		}
	}
}