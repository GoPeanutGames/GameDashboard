using System;
using System.Collections.Generic;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSLifeController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private List<Animator> _heartAnimators;

		[SerializeField]
		private RPSUserType _heartUserType;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private int _heartToBurst;
		
		private static readonly int Burst = Animator.StringToHash("Burst");
		private static readonly int Reset = Animator.StringToHash("Reset");

		private void OnEnable()
		{
			_heartToBurst = _heartAnimators.Count - 1;
			RPSLifeGameEvents.OnBurstHeart += OnBurstHeart;
			RPSLifeGameEvents.OnResetHearts += ResetHearts;
		}

		private void OnDisable()
		{
			RPSLifeGameEvents.OnBurstHeart -= OnBurstHeart;
			RPSLifeGameEvents.OnResetHearts -= ResetHearts;
		}

		private void ResetHearts()
		{
			LoggerService.LogInfo($"{nameof(RPSLifeController)}::{nameof(ResetHearts)}");
			_heartToBurst = _heartAnimators.Count - 1;
			foreach (Animator heartAnimator in _heartAnimators){
				heartAnimator.SetTrigger(Reset);
			}
		}

		private void OnBurstHeart(RPSUserType rpsUserType)
		{
			LoggerService.LogInfo($"{nameof(RPSLifeController)}::{nameof(OnBurstHeart)} - {rpsUserType}");
			if (rpsUserType != _heartUserType || _heartToBurst < 0){
				return;
			}
			LoggerService.LogInfo($"{nameof(RPSLifeController)}::{nameof(OnBurstHeart)} - bursting for: {_heartUserType}");
			_heartAnimators[_heartToBurst].SetTrigger(Burst);
			_heartToBurst--;
		}
	}
}