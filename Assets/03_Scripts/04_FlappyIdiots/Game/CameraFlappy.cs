using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class CameraFlappy : MonoBehaviour
    {
        Rect originalRect;
        float lastWindowAspect = 0f;
        float originalSize;
        public BackgroundLoop backgroundLoop;
        void Start()
        {
            originalRect = GetComponent<Camera>().rect;
            originalSize = GetComponent<Camera>().orthographicSize;
        }
        public float cameraAdjustmentStrength = 12f;
        public float landscapeAjudstmentStrength = 1f;
        private void Update()
        {
            UpdateCameraAspect();
        }
        void UpdateCameraAspect()
        {
            // Calculate the aspect ratio of the game window
            float windowAspect = (float)Screen.width / Screen.height;
            float targetAspect = 16f / 9f;
            if (lastWindowAspect != windowAspect)
            {
                lastWindowAspect = windowAspect;
                if (windowAspect < targetAspect)
                {
                    float scaleHeight = windowAspect / targetAspect;

                    GetComponent<Camera>().orthographicSize = originalSize + cameraAdjustmentStrength * (1 - scaleHeight);
                    var newScale = 1 + landscapeAjudstmentStrength * (1 - scaleHeight);
                    backgroundLoop.gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
                }
                else
                {
                    backgroundLoop.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    GetComponent<Camera>().orthographicSize = originalSize;
                }
            }
        }
    }
}