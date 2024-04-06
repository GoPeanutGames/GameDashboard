using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._03_RockPaperScissors.Events
{
	public static class RPSServerEvents
	{
		private static UnityAction<ulong, bool, bool, RPSChoiceType> _sendOtherChoiceToPlayer;
		private static UnityAction<RPSChoiceType, bool, bool> _opponentChoice;
		private static UnityAction _sendChoiceToServer;
        
		public static event UnityAction<ulong, bool, bool, RPSChoiceType> SendOtherChoiceToPlayer
		{
			add => _sendOtherChoiceToPlayer += value;
			remove => _sendOtherChoiceToPlayer -= value;
		}
		
		public static event UnityAction<RPSChoiceType, bool, bool> OpponentChoice
		{
			add => _opponentChoice += value;
			remove => _opponentChoice -= value;
		}
		
		public static event UnityAction SendChoiceToServer
		{
			add => _sendChoiceToServer += value;
			remove => _sendChoiceToServer -= value;
		}

		public static void RaiseSendOtherChoiceToPlayerEvent(ulong clientId, bool wonGame, bool endGame, RPSChoiceType choiceType)
		{
			if (_sendOtherChoiceToPlayer == null){
				LoggerService.LogWarning($"{nameof(RPSServerEvents)}::{nameof(RaiseSendOtherChoiceToPlayerEvent)} raised, but nothing picked it up");
				return;
			}
			_sendOtherChoiceToPlayer.Invoke(clientId, wonGame, endGame, choiceType);
		}
		
		public static void RaiseOpponentChoiceEvent(RPSChoiceType choiceType, bool wonGame, bool endGame)
		{
			if (_opponentChoice == null){
				LoggerService.LogWarning($"{nameof(RPSServerEvents)}::{nameof(RaiseOpponentChoiceEvent)} raised, but nothing picked it up");
				return;
			}
			_opponentChoice.Invoke(choiceType, wonGame, endGame);
		}
		
		public static void RaiseSendChoiceToServerEvent()
		{
			if (_sendChoiceToServer == null){
				LoggerService.LogWarning($"{nameof(RPSServerEvents)}::{nameof(RaiseSendChoiceToServerEvent)} raised, but nothing picked it up");
				return;
			}
			_sendChoiceToServer.Invoke();
		}
	}
}