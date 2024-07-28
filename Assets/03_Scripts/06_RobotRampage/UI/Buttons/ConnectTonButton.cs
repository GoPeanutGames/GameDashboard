using System;
using System.Collections;
using System.Collections.Generic;
using PeanutDashboard._06_RobotRampage;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TonSdk.Connect;
using TonSdk.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectTonButton : MonoBehaviour
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

    [Header(InspectorNames.DebugDynamic)]
    [SerializeField]
    private Button _button;
        
    private void Awake()
    {
        _tonConnectHandler.RestoreConnectionOnAwake = !UserService.LoggedOut;
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        TonConnectHandler.OnProviderStatusChanged +=OnProviderStatusChange;
        _button.onClick.AddListener(ConnectTon);
    }

    private void OnDisable()
    {
        TonConnectHandler.OnProviderStatusChanged -=OnProviderStatusChange;
        _button.onClick.RemoveListener(ConnectTon);
    }

    private void ConnectTon()
    {

        if (_tonConnectHandler.tonConnect.IsConnected){
            Debug.LogError("TON CONNECTED");
            return;
        }
        StartCoroutine(_tonConnectHandler.LoadWallets("https://raw.githubusercontent.com/ton-blockchain/wallets-list/main/wallets-v2.json", LoadWalletsCallback));
    }
    
    private void LoadWalletsCallback(List<WalletConfig> wallets)
    {
        // Here you can do something with the wallets list
        // for example: add them to the connect modal window
        // Warning! Use coroutines to load data from the web
        StartCoroutine(LoadWalletsIntoModal(wallets));
    }


    private IEnumerator LoadWalletsIntoModal(List<WalletConfig> wallets)
    {
        Debug.Log($"{nameof(ConnectTonButton)}::{nameof(LoadWalletsIntoModal)}");
        _walletParent.Activate();
        foreach (WalletConfig t in wallets){
            if (!_allowedWalletNames.Contains(t.Name))
            {
                continue;
            }

            WalletConfig tempConfig = t;
            if (t.Name == "Tonkeeper")
            {
                if (t.JsBridgeKey != null && InjectedProvider.IsWalletInjected(t.JsBridgeKey))
                {
                    _tonkeeperWalletExtension.onClick.AddListener(()=> OpenWebWallet(tempConfig));
                }
                else
                {
                    _tonkeeperWalletApp.onClick.AddListener(()=> OpenWallet(tempConfig));
                }
            }
            else
            {
                _telegramWalletButton.onClick.AddListener(()=> OpenWallet(tempConfig));
            }
        }
        yield return null;
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
        if(_tonConnectHandler.tonConnect.IsConnected)
        {
            Debug.Log("Wallet connected. Address: " + wallet.Account.Address + ". Platform: " + wallet.Device.Platform +
                      "," + wallet.Device.AppName + "," + wallet.Device.AppVersion);
            string address = wallet.Account.Address.ToString(AddressType.Base64);
            TonEvents.RaiseTonWalletConnectedEvent(address);
            UserService.SetUserAddress(address);
            SceneManager.LoadScene(1);
        }
        else
        {
            _walletParent.Deactivate();
        }
    }

}
