using PeanutDashboard.Dashboard.Events;
using UnityEngine;

namespace PeanutDashboard.Dashboard.UI
{
    public class UIChangeWithLogIn: MonoBehaviour
    {
        public GameObject buttonLoggedInParent;
        public GameObject buttonLoggedOutParent;

        private void OnEnable()
        {
            DashboardUIEvents.Instance.showLogInUI += ChangeLogInUI;
            ChangeLogInUI(false);
        }

        private void ChangeLogInUI(bool loggedIn)
        {
            buttonLoggedInParent.SetActive(loggedIn);
            buttonLoggedOutParent.SetActive(!loggedIn);
        }

        private void OnDisable()
        {
            DashboardUIEvents.Instance.showLogInUI -= ChangeLogInUI;
        }
    }
}