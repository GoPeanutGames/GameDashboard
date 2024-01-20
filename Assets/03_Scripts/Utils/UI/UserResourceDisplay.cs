using PeanutDashboard.Shared.User;
using PeanutDashboard.Shared.User.Events;
using TMPro;
using UnityEngine;

namespace PeanutDashboard.Utils.UI
{
	public class UserResourceDisplay : MonoBehaviour
	{
		public TMP_Text bubblesText;
		public TMP_Text gemsText;

		private void OnEnable()
		{
			UserEvents.Instance.UserResourcesUpdated += OnUserResourcesChanged;
		}

		private void OnUserResourcesChanged()
		{
			bubblesText.text = UserService.Instance.GetUserBubbles().ToString();
			gemsText.text = UserService.Instance.GetUserGems().ToString();
		}

		private void OnDisable()
		{
			UserEvents.Instance.UserResourcesUpdated -= OnUserResourcesChanged;
		}
	}
}