using System;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class MenuAnimationController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private Animator _menuAnimator;

        [SerializeField]
        private GameObject _menuPopup;

        [SerializeField]
        private Animator _menuPopupAnimator;

        [SerializeField]
        private GameObject _heroesPopup;

        [SerializeField]
        private GameObject _shopPopup;

        [SerializeField]
        private GameObject _rankPopup;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private bool _menuOpen;

        private void OnEnable()
        {
            MenuEvents.OnOpenMenu += OnOpenMenu;
            MenuEvents.OnCloseMenu += OnCloseMenu;
        }

        private void OnDisable()
        {
            MenuEvents.OnOpenMenu -= OnOpenMenu;
            MenuEvents.OnCloseMenu -= OnCloseMenu;
        }

        private void OnOpenMenu(MenuType menuType)
        {
            if (_menuOpen)
            {
                return;
            }

            _menuOpen = true;
            _menuAnimator.SetBool("Open", true);
            _menuPopup.Activate();
            _menuPopupAnimator.SetBool("Open", true);
            //TODO: bring correct popup to center
            //TODO: if already open use Do tween to slide it in
            //TODO: it should be a wrapper that has all three of them?
            //TODO: tween should just bring one forward
        }

        private void OnCloseMenu()
        {
            if (!_menuOpen)
            {
                return;
            }

            _menuPopupAnimator.SetBool("Open", false);
            _menuAnimator.SetBool("Open", false);
            _menuOpen = false;
        }
    }
}