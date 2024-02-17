using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.EndGame
{
	public class BattleDashGoHomePopup: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _endGamePopupWrapper;
		
		private void OnEnable()
		{
			BattleDashClientUIEvents.OnOpenEndGamePopup += OnShowGoHomePopup;
			BattleDashClientUIEvents.OnCloseEndGamePopup += OnCloseGoHomePopup;
		}

		private void OnDisable()
		{
			BattleDashClientUIEvents.OnOpenEndGamePopup -= OnShowGoHomePopup;
			BattleDashClientUIEvents.OnCloseEndGamePopup -= OnCloseGoHomePopup;
		}

		private void OnShowGoHomePopup()
		{
			_endGamePopupWrapper.Activate();
		}
		
		private void OnCloseGoHomePopup()
		{
			_endGamePopupWrapper.Deactivate();
		}
	}
}