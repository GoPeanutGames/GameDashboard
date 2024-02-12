using System.Runtime.InteropServices;

namespace PeanutDashboard.Utils.WebGL
{
    public static class WebGLUtils
    {
        [DllImport("__Internal")]
        private static extern bool isMobile();


        public static bool IsMobile => isMobile();
    }
}