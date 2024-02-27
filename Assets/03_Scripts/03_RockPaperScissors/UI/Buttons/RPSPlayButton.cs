using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	[RequireComponent(typeof(Button))]
	public class RPSPlayButton: MonoBehaviour
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
			_button.onClick.AddListener(OnPlayButtonClick);
			RPSClientGameEvents.OnDisablePlayerChoices += OnDisablePlayButton;
			RPSClientGameEvents.OnEnablePlayerChoices += OnEnablePlayButton;
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnPlayButtonClick);
			RPSClientGameEvents.OnDisablePlayerChoices -= OnDisablePlayButton;
			RPSClientGameEvents.OnEnablePlayerChoices -= OnEnablePlayButton;
		}

		private void OnEnablePlayButton()
		{
			_button.interactable = true;
		}
		
		private void OnDisablePlayButton()
		{
			_button.interactable = false;
		}

		private void OnPlayButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSPlayButton)}::{nameof(OnPlayButtonClick)}");
			RPSUIEvents.RaisePlayButtonClickEvent();
			RPSClientGameEvents.RaisePlayChoiceSelectedEvent();
		}
	}
}