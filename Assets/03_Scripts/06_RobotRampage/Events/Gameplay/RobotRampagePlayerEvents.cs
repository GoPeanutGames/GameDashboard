﻿using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampagePlayerEvents
	{
		private static UnityAction _playerKilled;
		
		private static UnityAction<float> _addPlayerExperience;
		
		private static UnityAction<Vector3> _movementDirectionUpdated;
		
		public static event UnityAction OnPlayerKilled
		{
			add => _playerKilled += value;
			remove => _playerKilled -= value;
		}
		
		public static event UnityAction<float> OnAddPlayerExperience
		{
			add => _addPlayerExperience += value;
			remove => _addPlayerExperience -= value;
		}
		
		public static event UnityAction<Vector3> OnMovementDirectionUpdated
		{
			add => _movementDirectionUpdated += value;
			remove => _movementDirectionUpdated -= value;
		}
		
		public static void RaisePlayerKilledEvent()
		{
			if (_playerKilled == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePlayerEvents)}::{nameof(RaisePlayerKilledEvent)} raised, but nothing picked it up");
				return;
			}
			_playerKilled.Invoke();
		}
		
		public static void RaiseAddPlayerExperienceEvent(float exp)
		{
			if (_addPlayerExperience == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePlayerEvents)}::{nameof(RaiseAddPlayerExperienceEvent)} raised, but nothing picked it up");
				return;
			}
			_addPlayerExperience.Invoke(exp);
		}
		
		public static void RaiseMovementDirectionUpdated(Vector3 direction)
		{
			if (_movementDirectionUpdated == null){
				LoggerService.LogWarning($"{nameof(RobotRampagePlayerEvents)}::{nameof(RaiseMovementDirectionUpdated)} raised, but nothing picked it up");
				return;
			}
			_movementDirectionUpdated.Invoke(direction);
		}
	}
}