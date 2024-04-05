using System.Collections.Generic;
using System.Linq;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UnityServer
{
	public static class RPSServerLogic
	{
		private static Dictionary<ulong, RPSChoiceType> _playerChoiceMap = new Dictionary<ulong, RPSChoiceType>();
		private static Dictionary<ulong, int> _playerScores = new Dictionary<ulong, int>();
		private static int _playersPlayed = 0;

		public static void PlayerMadeChoice(RPSChoiceType choiceType, ulong clientId)
		{
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(PlayerMadeChoice)}");
			if (!_playerChoiceMap.ContainsKey(clientId)){
				_playersPlayed++;
				_playerChoiceMap[clientId] = choiceType;
				if (_playersPlayed == 2){
					_playersPlayed = 0;
					ReturnResult();
				}
			}
		}

		private static void ReturnResult()
		{
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(ReturnResult)}");
			ulong firstClientId = _playerChoiceMap.Keys.ToList()[0];
			ulong secondClientId = _playerChoiceMap.Keys.ToList()[1];
			RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(firstClientId, _playerChoiceMap[secondClientId]);
			RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(secondClientId, _playerChoiceMap[firstClientId]);
		}
	}
}