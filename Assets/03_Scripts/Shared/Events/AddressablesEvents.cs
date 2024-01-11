using System;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared.Events
{
    public class AddressablesEvents: Singleton<AddressablesEvents>
    {
        public Action addressablesInitialised;
        public Action<float> downloadPercentageUpdated;
    }
}