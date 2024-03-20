using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSLifeGameEvents
	{
		
		private static UnityAction<RPSUserType> _burstHeart;
		private static UnityAction _resetHearts;
        
		public static event UnityAction<RPSUserType> OnBurstHeart
		{
			add => _burstHeart += value;
			remove => _burstHeart -= value;
		}
		
		public static event UnityAction OnResetHearts
		{
			add => _resetHearts += value;
			remove => _resetHearts -= value;
		}

		public static void RaiseBurstHeartEvent(RPSUserType rpsUserType)
		{
			if (_burstHeart == null){
				LoggerService.LogWarning($"{nameof(RPSLifeGameEvents)}::{nameof(RaiseBurstHeartEvent)} raised, but nothing picked it up");
				return;
			}
			_burstHeart.Invoke(rpsUserType);
		}
		
		public static void RaiseResetHeartsEvent()
		{
			if (_resetHearts == null){
				LoggerService.LogWarning($"{nameof(RPSLifeGameEvents)}::{nameof(RaiseResetHeartsEvent)} raised, but nothing picked it up");
				return;
			}
			_resetHearts.Invoke();
		}
	}
}