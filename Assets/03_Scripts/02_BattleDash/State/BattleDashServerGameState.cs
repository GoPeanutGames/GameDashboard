#if SERVER
using PeanutDashboard._02_BattleDash.Events;
#endif
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.State
{
	public class BattleDashServerGameState: NetworkBehaviour
	{
		public static bool isPaused;
		
		private readonly NetworkVariable<bool> _gamePaused = new NetworkVariable<bool>(false);

		private void OnEnable()
		{
#if SERVER
			BattleDashServerGameStateEvents.OnPauseTriggered += OnPauseTriggered;
			BattleDashServerGameStateEvents.OnUnPauseTriggered += OnUnPauseTriggered;
#else
			_gamePaused.OnValueChanged += OnGamePausedValueChanged;
#endif
		}
		
		private void OnDisable()
		{
#if SERVER

			BattleDashServerGameStateEvents.OnPauseTriggered -= OnPauseTriggered;
			BattleDashServerGameStateEvents.OnUnPauseTriggered -= OnUnPauseTriggered;
#else
			_gamePaused.OnValueChanged -= OnGamePausedValueChanged;
#endif
		}

#if SERVER
		private void OnPauseTriggered()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerGameState)}::{nameof(OnPauseTriggered)}");
			isPaused = true;
			_gamePaused.Value = true;
			Time.timeScale = 0;
		}
		
		private void OnUnPauseTriggered()
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerGameState)}::{nameof(OnUnPauseTriggered)}");
			isPaused = false;
			_gamePaused.Value = false;
			Time.timeScale = 1;
		}
#else
		private void OnGamePausedValueChanged(bool previousValue, bool newValue)
		{
			LoggerService.LogInfo($"{nameof(BattleDashServerGameState)}::{nameof(OnGamePausedValueChanged)} - {newValue}");
			isPaused = newValue;
			Time.timeScale = newValue ? 0 : 1;
		}
#endif

	}
}