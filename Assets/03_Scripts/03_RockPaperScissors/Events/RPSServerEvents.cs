using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSServerEvents
	{
		private static UnityAction<ulong, RPSChoiceType> _sendOtherChoiceToPlayer;
		private static UnityAction<RPSChoiceType> _opponentChoice;
        
		public static event UnityAction<ulong, RPSChoiceType> SendOtherChoiceToPlayer
		{
			add => _sendOtherChoiceToPlayer += value;
			remove => _sendOtherChoiceToPlayer -= value;
		}
		
		public static event UnityAction<RPSChoiceType> OpponentChoice
		{
			add => _opponentChoice += value;
			remove => _opponentChoice -= value;
		}

		public static void RaiseSendOtherChoiceToPlayerEvent(ulong clientId, RPSChoiceType choiceType)
		{
			if (_sendOtherChoiceToPlayer == null){
				LoggerService.LogWarning($"{nameof(RPSServerEvents)}::{nameof(RaiseSendOtherChoiceToPlayerEvent)} raised, but nothing picked it up");
				return;
			}
			_sendOtherChoiceToPlayer.Invoke(clientId, choiceType);
		}
		
		public static void RaiseOpponentChoiceEvent(RPSChoiceType choiceType)
		{
			if (_opponentChoice == null){
				LoggerService.LogWarning($"{nameof(RPSServerEvents)}::{nameof(RaiseOpponentChoiceEvent)} raised, but nothing picked it up");
				return;
			}
			_opponentChoice.Invoke(choiceType);
		}
	}
}