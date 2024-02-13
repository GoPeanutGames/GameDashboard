using System.Runtime.InteropServices;

namespace PeanutDashboard.Utils.WebGL
{
    public static class WebGLUtils
    {
        [DllImport("__Internal")]
        private static extern bool IsMobile();

        [DllImport("__Internal")]
        private static extern void TriggerFullscreenJS();

#if !UNITY_EDITOR
        public static bool IsWebMobile => IsMobile();
#else
	    public static bool IsWebMobile => false;
#endif
    }
}