using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSModeButtonsController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Button _freeButton;
		
		[SerializeField]
		private Button _bettingButton;

		private void OnEnable()
		{
			_freeButton.onClick.AddListener(OnFreeButtonClick);
			_bettingButton.onClick.AddListener(OnBettingButtonClick);
		}

		private void OnDisable()
		{
			_freeButton.onClick.RemoveListener(OnFreeButtonClick);
			_bettingButton.onClick.RemoveListener(OnBettingButtonClick);
		}

		private void OnFreeButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSModeButtonsController)}::{nameof(OnFreeButtonClick)}");
			RPSCurrentClientState.rpsModeType = RPSModeType.Free;
            RPSUIEvents.RaiseShowChooseOpponentScreenEvent();
		}

		private void OnBettingButtonClick()
		{
			LoggerService.LogInfo($"{nameof(RPSModeButtonsController)}::{nameof(OnBettingButtonClick)}");
			RPSCurrentClientState.rpsModeType = RPSModeType.Betting;
            RPSUIEvents.RaiseShowChooseOpponentScreenEvent();
		}
	}
}