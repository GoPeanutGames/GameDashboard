using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._04_SpaceEscape.UI
{
    public class SpaceEscapeStartScreenPlay : MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private SceneInfo _sceneInfo;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(RunnerGamePlayButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RunnerGamePlayButtonClick);
        }

        private void RunnerGamePlayButtonClick()
        {
            SceneLoaderService.Instance.LoadAndOpenScene(_sceneInfo);
        }
    }
}

