using System;
using PeanutDashboard.Shared.Logging;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageTimerController: MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private float _timer = 0f;

        [SerializeField]
        private bool _timerStarted = false;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            RobotRampageTimerEvents.OnStartTimer += StartTimer;
        }

        private void OnDisable()
        {
            RobotRampageTimerEvents.OnStartTimer -= StartTimer;
        }

        private void Update()
        {
            if (!_timerStarted)
            {
                return;
            }
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = 0;
                _timerStarted = false;
                RobotRampageTimerEvents.RaiseTimerDoneEvent();
            }
            float _seconds = _timer % 60f;
            string seconds = $"{Mathf.Floor(_seconds):F0}";
            float _timerMins = _timer / 60f;
            string mins = Mathf.Floor(_timerMins) >= 1 ? $"{Mathf.Floor(_timerMins):F0}:" : "";
            _text.text = $"{mins}{seconds}";
            
        }

        private void StartTimer(float timer)
        {
            LoggerService.LogInfo($"{nameof(RobotRampageTimerController)}::{nameof(StartTimer)}");
            _timerStarted = true;
            _timer = timer;
        }
    }
}