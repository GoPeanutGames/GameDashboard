using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
    public static class MenuEvents
    {
        private static UnityAction<MenuType> _openMenu;
        private static UnityAction _closeMenu;

        public static event UnityAction<MenuType> OnOpenMenu
        {
            add => _openMenu += value;
            remove => _openMenu -= value;
        }
        
        public static event UnityAction OnCloseMenu
        {
            add => _closeMenu += value;
            remove => _closeMenu -= value;
        }
		
        public static void RaiseOpenMenuEvent(MenuType menuType)
        {
            if (_openMenu == null){
                LoggerService.LogWarning($"{nameof(MenuEvents)}::{nameof(RaiseOpenMenuEvent)} raised, but nothing picked it up");
                return;
            }
            _openMenu.Invoke(menuType);
        }
        
        public static void RaiseCloseMenuEvent()
        {
            if (_closeMenu == null){
                LoggerService.LogWarning($"{nameof(MenuEvents)}::{nameof(RaiseCloseMenuEvent)} raised, but nothing picked it up");
                return;
            }
            _closeMenu.Invoke();
        }
    }
}