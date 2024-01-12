using System;

namespace PeanutDashboard.Dashboard.Events
{
    public class DashboardUIEvents : Utils.Singleton<DashboardUIEvents>
    {
        public Action<bool> showLogInUI;
    }
}