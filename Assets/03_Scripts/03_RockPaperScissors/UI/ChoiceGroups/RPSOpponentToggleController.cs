using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSOpponentToggleController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Toggle _playerToggle;
		
		[SerializeField]
		private Toggle _pcToggle;

		private void OnEnable()
		{
			_playerToggle.onValueChanged.AddListener(OnPlayerToggleValueChange);
			_pcToggle.onValueChanged.AddListener(OnPCToggleValueChange);
		}

		private void OnDisable()
		{
			_playerToggle.onValueChanged.RemoveListener(OnPlayerToggleValueChange);
			_pcToggle.onValueChanged.RemoveListener(OnPCToggleValueChange);
		}

		private void OnPlayerToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSOpponentToggleController)}::{nameof(OnPlayerToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsOpponentType = RPSOpponentType.Player;
			}
		}

		private void OnPCToggleValueChange(bool value)
		{
			LoggerService.LogInfo($"{nameof(RPSOpponentToggleController)}::{nameof(OnPCToggleValueChange)} - {value}");
			if (value){
				RPSCurrentClientState.rpsOpponentType = RPSOpponentType.PC;
			}
		}
	}
}