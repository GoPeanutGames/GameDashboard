using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSModeToggleController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Toggle _freeToggle;
		
		[SerializeField]
		private Toggle _bettingToggle;

		private void OnEnable()
		{
			_freeToggle.onValueChanged.AddListener(OnFreeToggleValueChange);
			_bettingToggle.onValueChanged.AddListener(OnBettingToggleValueChange);
		}

		private void OnDisable()
		{
			_freeToggle.onValueChanged.RemoveListener(OnFreeToggleValueChange);
			_bettingToggle.onValueChanged.RemoveListener(OnBettingToggleValueChange);
		}

		private void OnFreeToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSModeToggleController)}::{nameof(OnFreeToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsModeType = RPSModeType.Free;
			}
		}

		private void OnBettingToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSModeToggleController)}::{nameof(OnBettingToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsModeType = RPSModeType.Betting;
			}
		}
	}
}