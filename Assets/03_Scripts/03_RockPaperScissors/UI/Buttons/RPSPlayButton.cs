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
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnPlayButtonClick);
		}

		private void OnPlayButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSPlayButton)}::{nameof(OnPlayButtonClick)}");
			RPSUIEvents.RaisePlayButtonClickEvent();
		}
	}
}