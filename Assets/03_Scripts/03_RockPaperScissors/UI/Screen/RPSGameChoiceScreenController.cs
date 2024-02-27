using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSGameChoiceScreenController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Button _backButton;
		
		private void OnEnable()
		{
			RPSUIEvents.OnPlayButtonClick += OnPlayButtonClick;
		}

		private void OnDisable()
		{
			RPSUIEvents.OnPlayButtonClick -= OnPlayButtonClick;
		}

		private void OnPlayButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSGameChoiceScreenController)}::{nameof(OnPlayButtonClick)}");
			_backButton.gameObject.Deactivate();
		}
	}
}