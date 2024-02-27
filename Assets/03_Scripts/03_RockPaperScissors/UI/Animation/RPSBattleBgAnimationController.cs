using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSBattleBgAnimationController: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Animator _animator;
		
		private static readonly int Open = Animator.StringToHash("Open");

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void OnEnable()
		{
			RPSClientGameEvents.OnStartBattleBgOpenAnimation += TriggerHideAnimation;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnStartBattleBgOpenAnimation -= TriggerHideAnimation;
		}

		public void OnShowAnimationDone()
		{
			LoggerService.LogInfo($"{nameof(RPSBattleBgAnimationController)}::{nameof(OnShowAnimationDone)}");
			RPSClientGameEvents.RaiseBattleBgCloseAnimationDoneEvent();
		}

		public void OnHideAnimationDone()
		{
			
			LoggerService.LogInfo($"{nameof(RPSBattleBgAnimationController)}::{nameof(OnHideAnimationDone)}");
			RPSClientGameEvents.RaiseBattleBgOpenAnimationDoneEvent();
		}

		private void TriggerHideAnimation()
		{
			LoggerService.LogInfo($"{nameof(RPSBattleBgAnimationController)}::{nameof(TriggerHideAnimation)}");
			_animator.SetTrigger(Open);
		}
	}
}