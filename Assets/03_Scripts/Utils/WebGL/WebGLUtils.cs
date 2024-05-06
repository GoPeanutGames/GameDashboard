using System.Runtime.InteropServices;

namespace PeanutDashboard.Utils.WebGL
{
    public static class WebGLUtils
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool IsMobile();

        public static bool IsWebMobile => IsMobile();
#else
        public static bool IsWebMobile => false;
#endif
    }
}