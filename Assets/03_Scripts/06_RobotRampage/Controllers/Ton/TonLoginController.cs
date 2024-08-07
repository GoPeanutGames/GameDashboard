using System;
using System.Collections.Generic;
using PeanutDashboard.Server;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TonSdk.Connect;
using UnityEngine;
using UnityEngine.Events;
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

        private void Awake()
        {
            _tonConnectHandler = GameObject.FindObjectOfType<TonConnectHandler>();
            _tonConnectHandler.RestoreConnectionOnAwake = false;
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
            Debug.Log($"{nameof(TonLoginController)}::{nameof(LoadWalletsIntoModal)}");
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
                        _tonkeeperWalletExtension.onClick.AddListener(() => GetTonPayload(OpenWebWallet, tempConfig));
                    }
                    else
                    {
                        _tonkeeperWalletApp.onClick.AddListener(() => GetTonPayload(OpenWallet, tempConfig));
                    }
                }
                else
                {
                    _telegramWalletButton.onClick.AddListener(() => GetTonPayload(OpenWallet, tempConfig));
                }
            }
        }
        
        private void GetTonPayload(UnityAction<WalletConfig, TonPayloadData> continueAuthCb, WalletConfig wallet)
        {
            ServerService.GetDataFromServer(TonAuthApi.GetVerifyProof,(data)=> OnGetPayloadSuccess(data, continueAuthCb, wallet));
        }

        private void OnGetPayloadSuccess(string response, UnityAction<WalletConfig, TonPayloadData> continueAuthCb, WalletConfig wallet)
        {
            TonPayloadData payloadData = JsonUtility.FromJson<TonPayloadData>(response);
            continueAuthCb?.Invoke(wallet, payloadData);
        }

        private async void OpenWallet(WalletConfig walletConfig, TonPayloadData tonPayloadData)
        {
            Debug.Log($"{nameof(TonLoginController)}::{nameof(OpenWallet)} - {walletConfig.Name}");
            string connectUrl = await _tonConnectHandler.tonConnect.Connect(walletConfig, new ConnectAdditionalRequest(){TonProof = tonPayloadData.payload});
            string escapedUrl = Uri.EscapeUriString(connectUrl);
            Application.OpenURL(escapedUrl);
        }

        private void OpenWebWallet(WalletConfig walletConfig, TonPayloadData tonPayloadData)
        {
            Debug.Log($"{nameof(TonLoginController)}::{nameof(OpenWebWallet)} - {walletConfig.Name}");
            _tonConnectHandler.tonConnect.Connect(walletConfig, new ConnectAdditionalRequest(){TonProof = tonPayloadData.payload});
        }

        private void OnProviderStatusChange(Wallet wallet)
        {
            if (_tonConnectHandler.tonConnect.IsConnected)
            {
                Debug.Log(
                    $"{nameof(TonLoginController)}::{nameof(OnProviderStatusChange)} - Wallet connected. Address: " +
                    wallet.Account.Address + ". Platform: " +
                    wallet.Device.Platform + "," + wallet.Device.AppName + "," + wallet.Device.AppVersion);
                if (wallet.TonProof == null)
                {
                    Debug.LogWarning($"{nameof(TonLoginController)}::{nameof(OnProviderStatusChange)} - Ton proof is null");
                    _tonConnectHandler.tonConnect.Disconnect();
                    return;
                }
                // string address = wallet.Account.Address.ToString(AddressType.Base64);
                // UserService.SetUserAddress(address);
                VerifyTonProof(wallet);
            }
            else
            {
                _walletParent.Deactivate();
            }
        }

        private void VerifyTonProof(Wallet wallet)
        {
            Debug.Log($"{nameof(TonLoginController)}::{nameof(VerifyTonProof)}");
            TonVerifyProofData tonVerifyProofData = new()
            {
                proof = new()
                {
                    domain = new()
                    {
                        lengthBytes = wallet.TonProof.DomainLen,
                        value = wallet.TonProof.DomainVal
                    },
                    payload = wallet.TonProof.Payload,
                    signature = Convert.ToBase64String(wallet.TonProof.Signature),
                    timestamp = wallet.TonProof.Timestamp
                },
                walletInfo = new()
                {
                    address = wallet.Account.Address.ToString(),
                    // chain = EnvironmentManager.Instance.LoggingEnabled ? "TESTNET" : wallet.Account.Chain.ToString(),
                    chain = wallet.Account.Chain.ToString(),
                    publicKey = wallet.Account.PublicKey.ToString(),
                    walletStateInit = wallet.Account.WalletStateInit,
                }
            };
            string formData = JsonUtility.ToJson(tonVerifyProofData);
            
            ServerService.PostDataToServer<TonAuthApi>(TonAuthApi.VerifyProof, formData,TonVerifyProofSuccess, TonVerifyProofFail);
        }

        private void TonVerifyProofSuccess(string response)
        {
            Debug.Log($"{nameof(TonLoginController)}::{nameof(TonVerifyProofSuccess)}");
            TonProofResponse tonProofResponse = JsonUtility.FromJson<TonProofResponse>(response);
            UserService.SetTonToken(tonProofResponse.token);
            RobotRampageAudioEvents.RaisePlaySfxOneShotEvent(_onSuccessfulConnectSfx, 1f);
            TonAuthEvents.RaiseTonWalletConnectedEvent();
            _tonConnectHandler.RestoreConnectionOnAwake = true;
            UserService.SetLoggedOut(false);
            TonDataService.GetAccountData();
            SceneManager.LoadScene(1);
        }

        private void TonVerifyProofFail(string response)
        {
            Debug.LogError($"{nameof(TonLoginController)}::{nameof(TonVerifyProofSuccess)} - {response}");
        }
    }
}