using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
    [RequireComponent(typeof(Button))]
	public class RPSStartButton: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _startSfx;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnStartButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnStartButtonClick);
		}

		private void OnStartButtonClick()
		{
			RPSAudioEvents.RaisePlaySfxEvent(_startSfx, 1f);
			RPSUIEvents.RaiseShowChooseModeScreenEvent();
		}
	}
}