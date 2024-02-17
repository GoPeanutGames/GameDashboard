using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI.EndGame
{
	[RequireComponent(typeof(Button))]
	public class BattleDashExitButton: MonoBehaviour
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
			_button.onClick.AddListener(OnExitGameButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnExitGameButtonClick);
		}

		private void OnExitGameButtonClick()
		{
			ClientActionEvents.RaisePlayerRequestDisconnectEvent();
		}
	}
}