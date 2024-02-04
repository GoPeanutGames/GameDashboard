using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player
{
	public class BattleDashPlayerReticule : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		public RectTransform _reticule;

		private void Update()
		{
			_reticule.anchoredPosition = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
			Vector2 target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ClientActionEvents.RaiseUpdatePlayerAimEvent(target);
		}
	}
}