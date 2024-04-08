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
		private static bool _firstClientWon = false;
		private static bool _secondClientWon = false;
		private static bool _endGame = false;

		public static void PlayerMadeChoice(RPSChoiceType choiceType, ulong clientId)
		{
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(PlayerMadeChoice)}");
			if (!_playerChoiceMap.ContainsKey(clientId)){
				Debug.Log($"{nameof(RPSServerLogic)}::{nameof(PlayerMadeChoice)} - doesn't contain key");
				_playersPlayed++;
				_playerChoiceMap[clientId] = choiceType;
				_playerScores.TryAdd(clientId, 0);
				if (_playersPlayed == 2){
					Debug.Log($"{nameof(RPSServerLogic)}::{nameof(PlayerMadeChoice)} - return result");
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
			int roundResult = CalculateResultForPlayerOne(_playerChoiceMap[firstClientId], _playerChoiceMap[secondClientId]);
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(ReturnResult)} - {roundResult}");
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(ReturnResult)} - first client score: {_playerScores[firstClientId]}");
			Debug.Log($"{nameof(RPSServerLogic)}::{nameof(ReturnResult)} - second client score: {_playerScores[secondClientId]}");
			if (roundResult == -1){
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(firstClientId, false, false, _playerChoiceMap[secondClientId]);
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(secondClientId, false, false, _playerChoiceMap[firstClientId]);
			}else if (roundResult == 0){
				_playerScores[secondClientId]++;
				bool endGame = _playerScores[secondClientId] == 3;
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(firstClientId, false, endGame, _playerChoiceMap[secondClientId]);
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(secondClientId, true, endGame, _playerChoiceMap[firstClientId]);
                if(endGame){
                    _playerScores.Clear();
				}
			}else if (roundResult == 1){
				_playerScores[firstClientId]++;
				bool endGame = _playerScores[firstClientId] == 3;
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(firstClientId, true, endGame, _playerChoiceMap[secondClientId]);
				RPSServerEvents.RaiseSendOtherChoiceToPlayerEvent(secondClientId, false, endGame, _playerChoiceMap[firstClientId]);
                if(endGame){
                    _playerScores.Clear();
				}
			}
			_playerChoiceMap.Clear();
		}
		
		private static int CalculateResultForPlayerOne(RPSChoiceType firstClientChoice, RPSChoiceType secondClientChoice)
		{
			switch (secondClientChoice){
				case RPSChoiceType.Paper:
					switch (firstClientChoice){
						case RPSChoiceType.Rock:
							return 0;
						default:
							return 1;
					}
					break;
				case RPSChoiceType.Rock:
					switch (firstClientChoice){
						case RPSChoiceType.Paper:
							return 1;
						default:
							return 0;
					}
					break;
				case RPSChoiceType.Scissors:
					switch (firstClientChoice){
						case RPSChoiceType.Paper:
							return 0;
						case RPSChoiceType.Rock:
							return 1;
					}
					break;
			}
			return -1;
		}
	}
}