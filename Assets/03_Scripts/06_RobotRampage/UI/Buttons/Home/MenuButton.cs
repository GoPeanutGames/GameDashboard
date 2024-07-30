using DG.Tweening;
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

        [SerializeField]
        private Image _image;

        [SerializeField]
        private bool _active;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnHeroesButtonClick);
            MenuEvents.OnOpenMenu += OnMenuOpen;
            MenuEvents.OnCloseMenu += OnCloseMenu;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnHeroesButtonClick);
            MenuEvents.OnOpenMenu -= OnMenuOpen;
            MenuEvents.OnCloseMenu -= OnCloseMenu;
        }

        private void OnHeroesButtonClick()
        {
            MenuEvents.RaiseOpenMenuEvent(_menuType);
        }

        private void OnMenuOpen(MenuType menuType)
        {
            if (_menuType == MenuType.Rank)
            {
                return;
            }

            _active = _menuType == menuType;
            if (_active)
            {
                this.GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 1f), 0.5f);
            }
            else
            {
                this.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1f), 0.5f);
            }
            
            if (_menuType == MenuType.Rank)
            {
                _image.DOFade(0, 0.5f);
                _image.raycastTarget = false;
            }
            else if (_menuType == MenuType.Boost)
            {
                _image.raycastTarget = true;
                _image.DOFade(1, 0.5f);
            }
        }

        private void OnCloseMenu()
        {
            this.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1f), 0.5f);
            if (_menuType == MenuType.Rank)
            {
                _image.raycastTarget = true;
                _image.DOFade(1, 0.5f);
            }
            else if (_menuType == MenuType.Boost)
            {
                _image.raycastTarget = false;
                _image.DOFade(0, 0.5f);
            }
        }
    }
}