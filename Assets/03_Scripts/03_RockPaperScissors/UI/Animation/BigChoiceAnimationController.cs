using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	[RequireComponent(typeof(Animator))]
	public class BigChoiceAnimationController: MonoBehaviour
	{
		public void OnAnimationDone()
		{
			LoggerService.LogInfo($"{nameof(BigChoiceAnimationController)}::{nameof(OnAnimationDone)}");
			Invoke(nameof(InvokeEvent), 0.2f);
		}

		public void InvokeEvent()
		{
			LoggerService.LogInfo($"{nameof(BigChoiceAnimationController)}::{nameof(InvokeEvent)}");
			RPSClientGameEvents.RaiseSelectedChoiceAnimationDoneEvent();
		}
	}
}