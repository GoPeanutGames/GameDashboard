using System;
using PeanutDashboard.Dashboard.Events;
using UnityEngine;

namespace PeanutDashboard.Dashboard.UI.Auth
{
    public class UIChangeWithLogIn: MonoBehaviour
    {
        public GameObject buttonLoggedInParent;
        public GameObject buttonLoggedOutParent;

        private void Start()
        {
            ChangeLogInUI(false);
        }

        private void OnEnable()
        {
            DashboardUIEvents.Instance.showLogInUI += ChangeLogInUI;
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