using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI.EndGame
{
	[RequireComponent(typeof(Button))]
	public class BattleDashCloseEndGamePopupButton: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnCloseEndGamePopupButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnCloseEndGamePopupButtonClick);
		}

		private void OnCloseEndGamePopupButtonClick()
		{
			BattleDashClientUIEvents.RaiseCloseEndGamePopupEvent();
		}
	}
}