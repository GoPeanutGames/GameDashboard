using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSChoiceButtonsController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Toggle _rockToggle;
		
		[SerializeField]
		private Toggle _paperToggle;
		
		[SerializeField]
		private Toggle _scissorsToggle;
		
		[SerializeField]
		private Button _backButton;

		private void OnEnable()
		{
			_rockToggle.onValueChanged.AddListener(OnRockToggleValueChange);
			_paperToggle.onValueChanged.AddListener(OnPaperToggleValueChange);
			_scissorsToggle.onValueChanged.AddListener(OnScissorsToggleValueChange);
			_backButton.onClick.AddListener(OnBackButtonClick);
		}

		private void OnDisable()
		{
			_rockToggle.onValueChanged.RemoveListener(OnRockToggleValueChange);
			_paperToggle.onValueChanged.RemoveListener(OnPaperToggleValueChange);
			_scissorsToggle.onValueChanged.RemoveListener(OnScissorsToggleValueChange);
			_backButton.onClick.RemoveListener(OnBackButtonClick);
		}

		private void OnBackButtonClick()
		{
			RPSUIEvents.RaiseShowChooseOpponentScreenEvent();
		}
		
		private void OnRockToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSChoiceButtonsController)}::{nameof(OnRockToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsChoiceType = RPSChoiceType.Rock;
			}
		}

		private void OnPaperToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSChoiceButtonsController)}::{nameof(OnPaperToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsChoiceType = RPSChoiceType.Paper;
			}
		}
		
		private void OnScissorsToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSChoiceButtonsController)}::{nameof(OnScissorsToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsChoiceType = RPSChoiceType.Scissors;
			}
		}
	}
}