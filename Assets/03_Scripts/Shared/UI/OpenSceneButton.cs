using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.User;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Shared.UI
{
    public class OpenSceneButton : MonoBehaviour
    {
        [Header("Set In Inspector")]
        [SerializeField]
        private SceneInfo _gameScene;

        [Header("Debug Dynamic")]
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {UserService.Instance}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {UserService.Instance.IsLoggedIn()}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {SceneLoaderEvents.Instance}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {_gameScene}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {_gameScene.key}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {_gameScene.label}");
            Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}- {_gameScene.name}");
            if (UserService.Instance.IsLoggedIn()){
                SceneLoaderEvents.Instance.RaiseLoadAndOpenSceneEvent(_gameScene);
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnPlayButtonClick);
        }
    }
}

