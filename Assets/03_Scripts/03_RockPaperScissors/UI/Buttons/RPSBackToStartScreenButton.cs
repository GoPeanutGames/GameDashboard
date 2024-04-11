using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._03_RockPaperScissors.UI
{
    public class RPSBackToStartScreenButton: MonoBehaviour
    {
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnBackToStartScreenClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnBackToStartScreenClick);
        }
        
        private void OnBackToStartScreenClick()
        {
            RPSUIEvents.RaiseShowStartScreenEvent();
        }
    }
}