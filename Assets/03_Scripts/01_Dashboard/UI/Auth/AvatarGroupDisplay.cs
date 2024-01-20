using PeanutDashboard.Dashboard.Events;
using PeanutDashboard.Shared.User;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Dashboard.UI.Auth
{
	public class AvatarGroupDisplay : MonoBehaviour
	{
		[Header("UI")]
		[SerializeField]
		private Image _avatarImage;
		[SerializeField]
		private TMP_Text _notLoggedInText;
		[SerializeField]
		private TMP_Text _loggedInText;

		[Header("References")]
		[SerializeField]
		private Sprite _notLoggedInAvatarSprite;
		[SerializeField]
		private Sprite _loggedInAvatarSprite;
		[SerializeField]
		[TextArea(2,10)]
		private string _logInText;

		private void Start()
		{
			ChangeLogInUI(false);
		}

		private void OnEnable()
		{
			DashboardUIEvents.Instance.showLogInUI += ChangeLogInUI;
		}

		private void ChangeLogInUI(bool loggedIn)
		{
			_avatarImage.sprite = loggedIn ? _loggedInAvatarSprite : _notLoggedInAvatarSprite;
			_notLoggedInText.gameObject.SetActive(!loggedIn);
			_loggedInText.gameObject.SetActive(loggedIn);
			if (loggedIn){
				_loggedInText.text = ParseAddress(UserService.Instance.GetUserAddress(), _logInText);
			}
		}

		private static string ParseAddress(string address, string text)
		{
			return text.Replace("{address}", $"{address.Substring(0, 6)}...{address.Substring(address.Length - 4)}");
		}

		private void OnDisable()
		{
			DashboardUIEvents.Instance.showLogInUI -= ChangeLogInUI;
		}
	}
}