using System;
using PeanutDashboard.Utils;

namespace PeanutDashboard.Shared.Events
{
    public class SceneLoaderEvents : Singleton<SceneLoaderEvents>
    {
        public Action<float> sceneLoadProgressUpdated;
        public Action sceneLoaded;
    }
}

