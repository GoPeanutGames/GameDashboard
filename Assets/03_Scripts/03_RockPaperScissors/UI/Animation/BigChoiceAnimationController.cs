using PeanutDashboard._03_RockPaperScissors.Events;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	[RequireComponent(typeof(Animator))]
	public class BigChoiceAnimationController: MonoBehaviour
	{
		public void OnAnimationDone()
		{
			RPSClientGameEvents.RaiseSelectedChoiceAnimationDoneEvent();
		}
	}
}