using System;
using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		
		[SerializeField]
		private Image _playerChoiceImage;
		
		[SerializeField]
		private Image _enemyChoiceImage;

		private void OnEnable()
		{
			RPSUpperUIEvents.OnUpdateUpperBigText += OnUpdateUpperBigText;
			RPSUpperUIEvents.OnUpdateUpperSmallText += OnUpdateUpperSmallText;
			RPSUpperUIEvents.OnUpdateEnemyNameText += OnUpdateEnemyNameText;
			RPSUpperUIEvents.OnUpdateYourScoreText += OnUpdatePlayerScoreText;
			RPSUpperUIEvents.OnUpdateEnemyScoreText += OnUpdateEnemyScoreText;
			RPSUpperUIEvents.OnUpdateYourChoiceImage += OnUpdatePlayerChoiceImage;
			RPSUpperUIEvents.OnUpdateEnemyChoiceImage += OnUpdateEnemyChoiceImage;
			RPSUpperUIEvents.OnShowYourScore += OnShowPlayerScore;
			RPSUpperUIEvents.OnShowEnemyScore += OnShowEnemyScore;
			RPSUpperUIEvents.OnHideYourScore += OnHidePlayerScore;
			RPSUpperUIEvents.OnHideEnemyScore += OnHideEnemyScore;
		}

		private void OnDisable()
		{
			RPSUpperUIEvents.OnUpdateUpperBigText -= OnUpdateUpperBigText;
			RPSUpperUIEvents.OnUpdateUpperSmallText -= OnUpdateUpperSmallText;
			RPSUpperUIEvents.OnUpdateEnemyNameText -= OnUpdateEnemyNameText;
			RPSUpperUIEvents.OnUpdateYourScoreText -= OnUpdatePlayerScoreText;
			RPSUpperUIEvents.OnUpdateEnemyScoreText -= OnUpdateEnemyScoreText;
			RPSUpperUIEvents.OnUpdateYourChoiceImage -= OnUpdatePlayerChoiceImage;
			RPSUpperUIEvents.OnUpdateEnemyChoiceImage -= OnUpdateEnemyChoiceImage;
			RPSUpperUIEvents.OnShowYourScore -= OnShowPlayerScore;
			RPSUpperUIEvents.OnShowEnemyScore -= OnShowEnemyScore;
			RPSUpperUIEvents.OnHideYourScore -= OnHidePlayerScore;
			RPSUpperUIEvents.OnHideEnemyScore -= OnHideEnemyScore;
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

		private void OnUpdatePlayerChoiceImage(Sprite image)
		{
			_playerChoiceImage.sprite = image;
		}
		
		private void OnUpdateEnemyChoiceImage(Sprite image)
		{
			_enemyChoiceImage.sprite = image;
		}

		private void OnHidePlayerScore()
		{
			_playerScoreText.gameObject.Deactivate();
			_playerChoiceImage.gameObject.Activate();
		}

		private void OnShowPlayerScore()
		{
			_playerScoreText.gameObject.Activate();
			_playerChoiceImage.gameObject.Deactivate();
		}

		private void OnHideEnemyScore()
		{
			_enemyScoreText.gameObject.Deactivate();
			_enemyChoiceImage.gameObject.Activate();
		}

		private void OnShowEnemyScore()
		{
			_enemyScoreText.gameObject.Activate();
			_enemyChoiceImage.gameObject.Deactivate();
		}
	}
}