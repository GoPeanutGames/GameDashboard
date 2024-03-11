using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
	public class RPSBigChoiceController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private TMP_Text _text;

		[SerializeField]
		private Image _choiceImage;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private Sprite _rockSprite;
		
		[SerializeField]
		private Sprite _paperSprite;
		
		[SerializeField]
		private Sprite _scissorsSprite;
		
		private static readonly int Enlarge = Animator.StringToHash("Enlarge");
		private static readonly int Reset = Animator.StringToHash("Reset");

		private void OnEnable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected += OnPlayChoiceSelected;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone += OnSelectedChoiceAnimationDone;
		}

		private void OnDisable()
		{
			RPSClientGameEvents.OnPlayChoiceSelected -= OnPlayChoiceSelected;
			RPSClientGameEvents.OnSelectedChoiceAnimationDone -= OnSelectedChoiceAnimationDone;
		}

		private void OnPlayChoiceSelected()
		{
			_text.text = "You have picked";
			switch (RPSCurrentClientState.rpsChoiceType){
				case RPSChoiceType.Rock:
					_choiceImage.sprite = _rockSprite;
					break;
				case RPSChoiceType.Paper:
					_choiceImage.sprite = _paperSprite;
					break;
				case RPSChoiceType.Scissors:
					_choiceImage.sprite = _scissorsSprite;
					break;
			}
			_animator.SetTrigger(Enlarge);
		}

		private void OnSelectedChoiceAnimationDone()
		{
			_text.text = "";
			_animator.SetTrigger(Reset);
		}
	}
}