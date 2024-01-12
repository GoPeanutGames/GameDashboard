﻿using System;
using PeanutDashboard.Shared.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Dashboard.UI
{
    [RequireComponent(typeof(Button))]
    public class ConnectMetamaskButton: MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnConnectMetamaskButtonClick);
        }

        private void OnConnectMetamaskButtonClick()
        {
            AuthenticationService.Instance.StartMetamaskLogin();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnConnectMetamaskButtonClick);
        }
    }
}