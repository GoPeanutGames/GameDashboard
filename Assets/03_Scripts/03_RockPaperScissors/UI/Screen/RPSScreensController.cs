using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
    public class RPSScreensController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _chooseModeScreen;
        
        [SerializeField]
        private GameObject _chooseOpponentScreen;
        
        private void OnEnable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen += OnShowChooseOpponentScreen;
        }

        private void OnDisable()
        {
            RPSUIEvents.OnShowChooseOpponentScreen -= OnShowChooseOpponentScreen;
        }

        private void DisableAllScreens()
        {
            _chooseModeScreen.Deactivate();
            _chooseOpponentScreen.Deactivate();
        }
        
        private void OnShowChooseOpponentScreen()
        {
            DisableAllScreens();
            _chooseOpponentScreen.Activate();
        }
    }
}