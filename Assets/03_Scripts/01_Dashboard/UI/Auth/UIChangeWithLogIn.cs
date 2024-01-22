using PeanutDashboard.Dashboard.Events;
using UnityEngine;

namespace PeanutDashboard.Dashboard.UI.Auth
{
	public class UIChangeWithLogIn : MonoBehaviour
	{
		public GameObject buttonLoggedInParent;
		public GameObject buttonLoggedOutParent;

		private void Awake()
		{
			ChangeLogInUI(false);
		}

		private void OnEnable()
		{
			DashboardUIEvents.Instance.ShowLogInUI += ChangeLogInUI;
		}

		private void ChangeLogInUI(bool loggedIn)
		{
			buttonLoggedInParent.SetActive(loggedIn);
			buttonLoggedOutParent.SetActive(!loggedIn);
		}

		private void OnDisable()
		{
			DashboardUIEvents.Instance.ShowLogInUI -= ChangeLogInUI;
		}
	}
}