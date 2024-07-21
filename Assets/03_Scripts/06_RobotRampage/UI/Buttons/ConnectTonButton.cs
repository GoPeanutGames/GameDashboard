using System.Collections;
using System.Collections.Generic;
using PeanutDashboard._06_RobotRampage;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TMPro;
using TonSdk.Connect;
using TonSdk.Core;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectTonButton : MonoBehaviour
{
    [Header(InspectorNames.SetInInspector)]
    [SerializeField]
    private TonConnectHandler _tonConnectHandler;

    [SerializeField]
    private GameObject _walletParent;
    
    [SerializeField]
    private GameObject _walletConnectButtonPrefab;

    [Header(InspectorNames.DebugDynamic)]
    [SerializeField]
    private List<GameObject> _walletButtons;
    
    [SerializeField]
    private Button _button;
        
    private void Awake()
    {
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
        foreach (GameObject walletButton in _walletButtons){
            Destroy(walletButton);
        }
        _walletButtons.Clear();
        foreach (WalletConfig t in wallets){
            GameObject walletButton = Instantiate(_walletConnectButtonPrefab, _walletParent.transform);
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(t.Image))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) 
                    Debug.LogError("Error while loading wallet image: " + request.error);
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    if (texture != null && walletButton != null){
                        walletButton.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                        walletButton.GetComponentInChildren<Image>().preserveAspect = true;
                    }
                }
            }
            walletButton.GetComponentInChildren<TMP_Text>().text = t.Name;
            WalletConfig tempConfig = t;
            walletButton.GetComponentInChildren<Button>().onClick.AddListener(()=> OpenWallet(tempConfig));
            _walletButtons.Add(walletButton);
        }
        yield return null;
    }

    private void OpenWallet(WalletConfig walletConfig)
    {
        Debug.Log($"{nameof(ConnectTonButton)}::{nameof(OpenWallet)} - {walletConfig.Name}");
        _tonConnectHandler.tonConnect.Connect(walletConfig);
        _walletParent.Deactivate();
    }

    private void OnProviderStatusChange(Wallet wallet)
    {
        if(_tonConnectHandler.tonConnect.IsConnected)
        {
            Debug.Log("Wallet connected. Address: " + wallet.Account.Address + ". Platform: " + wallet.Device.Platform + "," + wallet.Device.AppName + "," + wallet.Device.AppVersion);
            //TODO: we're conencted
            TonEvents.RaiseTonWalletConnectedEvent(wallet.Account.Address.ToString(AddressType.Base64));
            // EnableWalletInfoButton(ProcessWalletAddress(wallet.Account.Address.ToString(AddressType.Base64)));
        }
        else
        {
            //TODO: we're not connected
        }
    }

}
