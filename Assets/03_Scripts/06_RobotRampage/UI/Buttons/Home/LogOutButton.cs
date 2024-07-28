using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class LogOutButton: MonoBehaviour
    {
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnLoggedOutClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnLoggedOutClick);
        }

        private void OnLoggedOutClick()
        {
            UserService.SetLoggedOut(true);
            SceneManager.LoadScene(0);
        }
    }
}