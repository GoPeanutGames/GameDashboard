using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.User;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Shared.UI
{
    [RequireComponent(typeof(Button))]
    public class OpenSceneButton : MonoBehaviour
    {
        [Header("Set In Inspector")]
        [SerializeField]
        private SceneInfo _sceneInfo;

        [Header("Debug Dynamic")]
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnButtonClick)}");
            if (UserService.Instance.IsLoggedIn()){
                SceneLoaderEvents.Instance.RaiseLoadAndOpenSceneEvent(_sceneInfo);
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}

