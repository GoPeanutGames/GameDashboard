﻿using PeanutDashboard.Utils.Misc;
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
		}

		private void OnDisable()
		{
			_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}

		private void OnToggleValueChanged(bool value)
		{
			_toggle.image.sprite = value ? _selectedSprite : _deSelectedSprite;
			_text.font = value ? _selectedTextFont : _normalTextFont;
		}
	}
}