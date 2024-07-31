using PeanutDashboard.Utils;
using TonSdk.Connect;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeanutDashboard._06_RobotRampage
{
    public class TonWrapper: MonoSingleton<TonWrapper>
    {
        [SerializeField]
        private TonConnectHandler _tonConnectHandler;
        
        private void OnEnable()
        {
            TonConnectHandler.OnProviderStatusChanged += OnProviderStatusChange;
            TonAuthEvents.OnTonDisconnect += DisconnectTon;
        }

        private void OnDisable()
        {
            TonConnectHandler.OnProviderStatusChanged -= OnProviderStatusChange;
            TonAuthEvents.OnTonDisconnect -= DisconnectTon;
        }

        private void DisconnectTon()
        {
            UserService.SetLoggedOut(true);
            _tonConnectHandler.RestoreConnectionOnAwake = false;
            _tonConnectHandler.tonConnect.Disconnect();
            SceneManager.LoadScene(0);
        }
        
        private void OnProviderStatusChange(Wallet wallet)
        {
            if (!UserService.LoggedOut && !_tonConnectHandler.tonConnect.IsConnected)
            {
                UserService.SetLoggedOut(true);
                SceneManager.LoadScene(0);
            }
        }
    }
}