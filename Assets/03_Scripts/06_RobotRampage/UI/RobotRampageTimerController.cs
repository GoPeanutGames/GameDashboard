using System;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage.UI
{
    public class RobotRampageTimerController: MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private float _timer = 0f;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            float _seconds = _timer % 60f;
            string seconds = $"{Mathf.Floor(_seconds):F0}";
            float _timerMins = _timer / 60f;
            string mins = Mathf.Floor(_timerMins) >= 1 ? $"{Mathf.Floor(_timerMins):F0}:" : "";
            _text.text = $"{mins}{seconds}";
        }
    }
}