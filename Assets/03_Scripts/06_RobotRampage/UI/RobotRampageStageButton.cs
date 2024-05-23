using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageStageButton: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private RobotRampageStageData _stageData;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnStageButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnStageButtonClick);
        }

        private void OnStageButtonClick()
        {
            RobotRampageStageService.currentStageData = _stageData;
            SceneManager.LoadScene(0);
        }
    }
}