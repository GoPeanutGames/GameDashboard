using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI
{
	[RequireComponent(typeof(Button))]
	public class CloseTooltipButton: MonoBehaviour
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
			_button.onClick.AddListener(OnCloseTooltipsButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnCloseTooltipsButtonClick);
		}

		private void OnCloseTooltipsButtonClick()
		{
			BattleDashClientUIEvents.RaiseHideTooltipsEvent();
		}
	}
}