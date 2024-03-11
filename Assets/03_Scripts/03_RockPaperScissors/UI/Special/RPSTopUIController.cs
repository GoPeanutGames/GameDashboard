using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Shared.Logging;
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
		
		[SerializeField]
		private Image _centralIndicator;

		private void OnEnable()
		{
			RPSUpperUIEvents.OnUpdateUpperBigText += OnUpdateUpperBigText;
			RPSUpperUIEvents.OnUpdateUpperSmallText += OnUpdateUpperSmallText;
			RPSUpperUIEvents.OnUpdateEnemyNameText += OnUpdateEnemyNameText;
			RPSUpperUIEvents.OnUpdateYourScoreText += OnUpdatePlayerScoreText;
			RPSUpperUIEvents.OnUpdateEnemyScoreText += OnUpdateEnemyScoreText;
			RPSUpperUIEvents.OnUpdateYourChoiceImage += OnUpdatePlayerChoiceImage;
			RPSUpperUIEvents.OnUpdateEnemyChoiceImage += OnUpdateEnemyChoiceImage;
			RPSUpperUIEvents.OnUpdateUpperIndicator += OnUpdateUpperIndicator;
			RPSUpperUIEvents.OnShowYourScore += OnShowPlayerScore;
			RPSUpperUIEvents.OnShowEnemyScore += OnShowEnemyScore;
			RPSUpperUIEvents.OnHideYourScore += OnHidePlayerScore;
			RPSUpperUIEvents.OnHideEnemyScore += OnHideEnemyScore;
			RPSUpperUIEvents.OnHideIndicator += OnHideIndicator;
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
			RPSUpperUIEvents.OnUpdateUpperIndicator -= OnUpdateUpperIndicator;
			RPSUpperUIEvents.OnShowYourScore -= OnShowPlayerScore;
			RPSUpperUIEvents.OnShowEnemyScore -= OnShowEnemyScore;
			RPSUpperUIEvents.OnHideYourScore -= OnHidePlayerScore;
			RPSUpperUIEvents.OnHideEnemyScore -= OnHideEnemyScore;
			RPSUpperUIEvents.OnHideIndicator -= OnHideIndicator;
		}
		
		private void OnUpdateUpperSmallText(string text)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateUpperSmallText)} - {text}");
			_centralUpperText.text = text;
		}

		private void OnUpdateUpperBigText(string text)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateUpperBigText)} - {text}");
			_centralLowerText.text = text;
		}

		private void OnUpdateEnemyNameText(string text)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateEnemyNameText)} - {text}");
			_enemyNameText.text = text;
		}
		
		private void OnUpdatePlayerScoreText(string text)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdatePlayerScoreText)} - {text}");
			_playerScoreText.text = text;
		}
		
		private void OnUpdateEnemyScoreText(string text)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateEnemyScoreText)} - {text}");
			_enemyScoreText.text = text;
		}

		private void OnUpdatePlayerChoiceImage(Sprite image)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdatePlayerChoiceImage)} - {image.name}");
			_playerChoiceImage.sprite = image;
		}
		
		private void OnUpdateEnemyChoiceImage(Sprite image)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateEnemyChoiceImage)} - {image.name}");
			_enemyChoiceImage.sprite = image;
		}

		private void OnHidePlayerScore()
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnHidePlayerScore)}");
			_playerScoreText.gameObject.Deactivate();
			_playerChoiceImage.gameObject.Activate();
		}

		private void OnShowPlayerScore()
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnShowPlayerScore)}");
			_playerScoreText.gameObject.Activate();
			_playerChoiceImage.gameObject.Deactivate();
		}

		private void OnHideEnemyScore()
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnHideEnemyScore)}");
			_enemyScoreText.gameObject.Deactivate();
			_enemyChoiceImage.gameObject.Activate();
		}

		private void OnShowEnemyScore()
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnShowEnemyScore)}");
			_enemyScoreText.gameObject.Activate();
			_enemyChoiceImage.gameObject.Deactivate();
		}

		private void OnUpdateUpperIndicator(Sprite indicator)
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnUpdateUpperIndicator)} - {indicator.name}");
			_centralIndicator.gameObject.Activate();
			_centralIndicator.sprite = indicator;
		}

		private void OnHideIndicator()
		{
			LoggerService.LogInfo($"{nameof(RPSTopUIController)}::{nameof(OnHideIndicator)}");
			_centralIndicator.gameObject.Deactivate();
			
		}
	}
}