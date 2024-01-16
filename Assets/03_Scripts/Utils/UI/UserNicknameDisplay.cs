using System;
using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;
using TMPro;
using UnityEngine;

namespace PeanutDashboard.Utils.UI
{
    public class UserNicknameDisplay: MonoBehaviour
    {
        public string textBefore;
        public TMP_Text nicknameDisplay;
        public string textAfter;
        
        private void OnEnable()
        {
            UserEvents.Instance.userGeneralInfoUpdated += OnUserGeneralInfoUpdated;
        }

        private void OnUserGeneralInfoUpdated()
        {
            nicknameDisplay.text = $"{textBefore}{UserService.Instance.GetUserNickname()}{textAfter}";
        }

        private void OnDisable()
        {
            UserEvents.Instance.userGeneralInfoUpdated -= OnUserGeneralInfoUpdated;
        }
    }
}