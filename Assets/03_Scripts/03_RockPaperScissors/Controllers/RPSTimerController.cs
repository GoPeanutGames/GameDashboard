using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSTimerController: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _timerLeft;

		[SerializeField]
		private bool _timerStarted;
		
		private void OnEnable()
		{
			RPSTimerEvents.OnStartTimer += OnStartTimer;
			RPSClientGameEvents.OnPlayChoiceSelected += OnStopTimer;
		}

		private void OnDisable()
		{
			RPSTimerEvents.OnStartTimer -= OnStartTimer;
			RPSClientGameEvents.OnPlayChoiceSelected -= OnStopTimer;
		}

		private void Update()
		{
			if (!_timerStarted){
				return;
			}
			_timerLeft -= Time.deltaTime;
			RPSUpperUIEvents.RaiseUpdateUpperBigTextEvent($"{_timerLeft:N0}");
			if (_timerLeft <= 0){
				_timerStarted = false;
				RPSChoiceType randomChoice = (RPSChoiceType)UnityEngine.Random.Range(0, 3);
				RPSCurrentClientState.rpsChoiceType = randomChoice;
				RPSClientGameEvents.RaisePlayChoiceSelectedEvent();
			}
		}

		private void OnStartTimer(float time)
		{
			_timerLeft = time;
			_timerStarted = true;
			RPSUpperUIEvents.RaiseUpdateUpperSmallTextEvent("STARTS IN...");
		}

		private void OnStopTimer()
		{
			_timerStarted = false;
		}
	}
}