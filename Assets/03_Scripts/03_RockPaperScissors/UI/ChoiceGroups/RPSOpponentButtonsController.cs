using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSOpponentButtonsController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Button _humanButton;
		
		[SerializeField]
		private Button _pcButton;
		
		[SerializeField]
		private Button _backButton;
		
		[SerializeField]
		private AudioClip _clickSfx;

		private void OnEnable()
		{
			_humanButton.onClick.AddListener(OnPlayerButtonClick);
			_pcButton.onClick.AddListener(OnPCButtonClick);
			_backButton.onClick.AddListener(OnBackButtonClick);
		}

		private void OnDisable()
		{
			_humanButton.onClick.RemoveListener(OnPlayerButtonClick);
			_pcButton.onClick.RemoveListener(OnPCButtonClick);
			_backButton.onClick.RemoveListener(OnBackButtonClick);
		}

		private void OnBackButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSOpponentButtonsController)}::{nameof(OnBackButtonClick)}");
			RPSUIEvents.RaiseShowChooseModeScreenEvent();
		}
		
		private void OnPlayerButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSOpponentButtonsController)}::{nameof(OnPlayerButtonClick)}");
			RPSCurrentClientState.rpsOpponentType = RPSOpponentType.Player;
            RPSUIEvents.RaiseGameShowChooseOptionScreenEvent();
            RPSAudioEvents.RaisePlaySfxEvent(_clickSfx, 1);
		}

		private void OnPCButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSOpponentButtonsController)}::{nameof(OnPCButtonClick)}");
			RPSCurrentClientState.rpsOpponentType = RPSOpponentType.PC;
            RPSUIEvents.RaiseGameShowChooseOptionScreenEvent();
            RPSAudioEvents.RaisePlaySfxEvent(_clickSfx, 1);
		}
	}
}