using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class MenuButton: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private MenuType _menuType;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnHeroesButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnHeroesButtonClick);
        }

        private void OnHeroesButtonClick()
        {
            MenuEvents.RaiseOpenMenuEvent(_menuType);
        }
    }
}