using UnityEngine;

namespace PeanutDashboard.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this.GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}