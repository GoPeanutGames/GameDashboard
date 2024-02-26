using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSTopUIController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private TMP_Text _centralUpperText;
		
		[SerializeField]
		private TMP_Text _centralLowerText;
		
		[SerializeField]
		private TMP_Text _enemyNameText;
		
		[SerializeField]
		private TMP_Text _playerScoreText;
		
		[SerializeField]
		private TMP_Text _enemyScoreText;

		private void OnEnable()
		{
			RPSUpperUIEvents.OnUpdateUpperBigText += OnUpdateUpperBigText;
			RPSUpperUIEvents.OnUpdateUpperSmallText += OnUpdateUpperSmallText;
			RPSUpperUIEvents.OnUpdateEnemyNameText += OnUpdateEnemyNameText;
			RPSUpperUIEvents.OnUpdateYourScoreText += OnUpdatePlayerScoreText;
			RPSUpperUIEvents.OnUpdateEnemyScoreText += OnUpdateEnemyScoreText;
		}

		private void OnDisable()
		{
			RPSUpperUIEvents.OnUpdateUpperBigText -= OnUpdateUpperBigText;
			RPSUpperUIEvents.OnUpdateUpperSmallText -= OnUpdateUpperSmallText;
			RPSUpperUIEvents.OnUpdateEnemyNameText -= OnUpdateEnemyNameText;
			RPSUpperUIEvents.OnUpdateYourScoreText -= OnUpdatePlayerScoreText;
			RPSUpperUIEvents.OnUpdateEnemyScoreText -= OnUpdateEnemyScoreText;
		}
		
		private void OnUpdateUpperSmallText(string text)
		{
			_centralUpperText.text = text;
		}

		private void OnUpdateUpperBigText(string text)
		{
			_centralLowerText.text = text;
		}

		private void OnUpdateEnemyNameText(string text)
		{
			_enemyNameText.text = text;
		}
		
		private void OnUpdatePlayerScoreText(string text)
		{
			_playerScoreText.text = text;
		}
		
		private void OnUpdateEnemyScoreText(string text)
		{
			_enemyScoreText.text = text;
		}
	}
}