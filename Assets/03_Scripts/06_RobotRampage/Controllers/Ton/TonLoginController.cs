using System;
using System.Collections.Generic;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TonSdk.Connect;
using TonSdk.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class TonLoginController : MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private TonConnectHandler _tonConnectHandler;

        [SerializeField]
        private GameObject _walletParent;

        [SerializeField]
        private Button _telegramWalletButton;

        [SerializeField]
        private Button _tonkeeperWalletExtension;

        [SerializeField]
        private Button _tonkeeperWalletApp;

        [SerializeField]
        private List<string> _allowedWalletNames;

        [SerializeField]
        private AudioClip _onSuccessfulConnectSfx;

        private void Start()
        {
            _tonConnectHandler = GameObject.FindObjectOfType<TonConnectHandler>();
        }

        private void OnEnable()
        {
            TonConnectHandler.OnProviderStatusChanged += OnProviderStatusChange;
            TonAuthEvents.OnTonConnect += ConnectTon;
        }

        private void OnDisable()
        {
            TonConnectHandler.OnProviderStatusChanged -= OnProviderStatusChange;
            TonAuthEvents.OnTonConnect -= ConnectTon;
        }

        private void ConnectTon()
        {
            if (_tonConnectHandler.tonConnect.IsConnected)
            {
                Debug.LogError("TON CONNECTED");
                return;
            }

            StartCoroutine(_tonConnectHandler.LoadWallets(
                "https://raw.githubusercontent.com/ton-blockchain/wallets-list/main/wallets-v2.json",
                LoadWalletsIntoModal));
        }

        private void LoadWalletsIntoModal(List<WalletConfig> wallets)
        {
            Debug.Log($"{nameof(ConnectTonButton)}::{nameof(LoadWalletsIntoModal)}");
            _walletParent.Activate();
            foreach (WalletConfig t in wallets)
            {
                Debug.Log(t.Name);
                if (!_allowedWalletNames.Contains(t.Name))
                {
                    continue;
                }

                WalletConfig tempConfig = t;
                if (t.Name == "Tonkeeper")
                {
                    if (t.JsBridgeKey != null && InjectedProvider.IsWalletInjected(t.JsBridgeKey))
                    {
                        _tonkeeperWalletExtension.onClick.AddListener(() => OpenWebWallet(tempConfig));
                    }
                    else
                    {
                        _tonkeeperWalletApp.onClick.AddListener(() => OpenWallet(tempConfig));
                    }
                }
                else
                {
                    _telegramWalletButton.onClick.AddListener(() => OpenWallet(tempConfig));
                }
            }
        }

        private async void OpenWallet(WalletConfig walletConfig)
        {
            Debug.Log($"{nameof(ConnectTonButton)}::{nameof(OpenWallet)} - {walletConfig.Name}");
            string connectUrl = await _tonConnectHandler.tonConnect.Connect(walletConfig);
            string escapedUrl = Uri.EscapeUriString(connectUrl);
            Application.OpenURL(escapedUrl);
        }

        private void OpenWebWallet(WalletConfig walletConfig)
        {
            Debug.Log($"{nameof(ConnectTonButton)}::{nameof(OpenWebWallet)} - {walletConfig.Name}");
            _tonConnectHandler.tonConnect.Connect(walletConfig);
        }

        private void OnProviderStatusChange(Wallet wallet)
        {
            if (_tonConnectHandler.tonConnect.IsConnected)
            {
                Debug.Log("Wallet connected. Address: " + wallet.Account.Address + ". Platform: " +
                          wallet.Device.Platform +
                          "," + wallet.Device.AppName + "," + wallet.Device.AppVersion);
                RobotRampageAudioEvents.RaisePlaySfxOneShotEvent(_onSuccessfulConnectSfx, 1f);
                string address = wallet.Account.Address.ToString(AddressType.Base64);
                TonAuthEvents.RaiseTonWalletConnectedEvent(address);
                UserService.SetUserAddress(address);
                _tonConnectHandler.RestoreConnectionOnAwake = true;
                UserService.SetLoggedOut(false);
                SceneManager.LoadScene(1);
            }
            else
            {
                _walletParent.Deactivate();
            }
        }
    }
}