#if SERVER
using PeanutDashboard._02_BattleDash.Events;
#endif
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.State
{
	public class ServerBattleDashGameState: NetworkBehaviour
	{
		public static bool isPaused;
		
		private readonly NetworkVariable<bool> _gamePaused = new NetworkVariable<bool>(false);

		private void OnEnable()
		{
#if SERVER
			ServerGameStateEvents.OnPauseTriggered += OnPauseTriggered;
			ServerGameStateEvents.OnUnPauseTriggered += OnUnPauseTriggered;
#else
			_gamePaused.OnValueChanged += OnGamePausedValueChanged;
#endif
		}
		
		private void OnDisable()
		{
#if SERVER

			ServerGameStateEvents.OnPauseTriggered -= OnPauseTriggered;
			ServerGameStateEvents.OnUnPauseTriggered -= OnUnPauseTriggered;
#else
			_gamePaused.OnValueChanged -= OnGamePausedValueChanged;
#endif
		}

#if SERVER
		private void OnPauseTriggered()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashGameState)}::{nameof(OnPauseTriggered)}");
			isPaused = true;
			_gamePaused.Value = true;
			Time.timeScale = 0;
		}
		
		private void OnUnPauseTriggered()
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashGameState)}::{nameof(OnUnPauseTriggered)}");
			isPaused = false;
			_gamePaused.Value = false;
			Time.timeScale = 1;
		}
#else
		private void OnGamePausedValueChanged(bool previousValue, bool newValue)
		{
			LoggerService.LogInfo($"{nameof(ServerBattleDashGameState)}::{nameof(OnGamePausedValueChanged)} - {newValue}");
			isPaused = newValue;
			Time.timeScale = newValue ? 0 : 1;
		}
#endif

	}
}