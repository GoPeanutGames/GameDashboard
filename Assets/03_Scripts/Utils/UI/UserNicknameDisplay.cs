using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;
using TMPro;
using UnityEngine;

namespace PeanutDashboard.Utils.UI
{
	public class UserNicknameDisplay : MonoBehaviour
	{
		[TextArea(3, 5)]
		public string textBefore;
		public TMP_Text nicknameDisplay;
		[TextArea(3, 5)]
		public string textAfter;

		private void OnEnable()
		{
			UserEvents.Instance.UserGeneralInfoUpdated += OnUserGeneralInfoUpdated;
			OnUserGeneralInfoUpdated();
		}

		private void OnUserGeneralInfoUpdated()
		{
			string userNickname = UserService.Instance.GetUserNickname();
			if (string.IsNullOrEmpty(userNickname)){
				nicknameDisplay.text = $"{textBefore}Peanut Gang{textAfter}";
				return;
			}
			nicknameDisplay.text = $"{textBefore}{UserService.Instance.GetUserNickname()}{textAfter}";
		}

		private void OnDisable()
		{
			UserEvents.Instance.UserGeneralInfoUpdated -= OnUserGeneralInfoUpdated;
		}
	}
}