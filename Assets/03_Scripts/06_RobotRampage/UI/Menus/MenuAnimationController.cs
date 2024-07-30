using System.Collections.Generic;
using DG.Tweening;
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
        private OnAnimationDoneTrigger _onMenuCloseAnimationDone;
        
        [SerializeField]
        private List<GameObject> _menuTypePopups;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private bool _menuOpen;

        [SerializeField]
        private int _currentMenuIndex;

        private void OnEnable()
        {
            MenuEvents.OnOpenMenu += OnOpenMenu;
            MenuEvents.OnCloseMenu += OnCloseMenu;
            _onMenuCloseAnimationDone.AddListener(OnCloseMenuDone);
        }

        private void OnDisable()
        {
            MenuEvents.OnOpenMenu -= OnOpenMenu;
            MenuEvents.OnCloseMenu -= OnCloseMenu;
            _onMenuCloseAnimationDone.RemoveListener(OnCloseMenuDone);
        }

        private void OnOpenMenu(MenuType menuType)
        {
            if (menuType == MenuType.Rank)
            {
                return;
            }
            if (_menuOpen)
            {
                SlideTo(menuType);
                return;
            }

            _menuOpen = true;
            _menuAnimator.SetBool("Open", true);
            _menuPopup.Activate();
            _menuPopupAnimator.SetBool("Open", true);
            _currentMenuIndex = (int)menuType;
            for (int i = 0; i < _menuTypePopups.Count; i++)
            {
                int diff = i - _currentMenuIndex; 
                RectTransform rectTransform = _menuTypePopups[i].GetComponent<RectTransform>();
                rectTransform.offsetMin = new Vector2(1046 * diff, 0);
                rectTransform.offsetMax = new Vector2(1046 * diff, 0);
            }
        }

        private void SlideTo(MenuType menuType)
        {
            _currentMenuIndex = (int)menuType;
            for (int i = 0; i < _menuTypePopups.Count; i++)
            {
                int diff = i - _currentMenuIndex; 
                RectTransform rectTransform = _menuTypePopups[i].GetComponent<RectTransform>();
                rectTransform.DOLocalMove(new Vector3(1046 * diff, 0, 0), 0.3f);
            }            
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

        private void OnCloseMenuDone()
        {
            _menuPopup.Deactivate();
        }
    }
}