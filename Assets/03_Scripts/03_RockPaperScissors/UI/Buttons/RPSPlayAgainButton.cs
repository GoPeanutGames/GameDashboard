﻿using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	[RequireComponent(typeof(Button))]
	public class RPSPlayAgainButton: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnPlayAgainButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnPlayAgainButtonClick);
		}

		private void OnPlayAgainButtonClick()
		{
			RPSUIEvents.RaiseShowStartScreenEvent();
			RPSUIEvents.RaiseHideResultScreens();
		}
	}
}