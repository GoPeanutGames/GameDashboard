using PeanutDashboard.Shared.Metamask;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Dashboard.UI.Metamask
{
	[RequireComponent(typeof(Button))]
	public class DisconnectMetamaskButton: MonoBehaviour
	{
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnDisconnectMetamaskButtonClick);
		}

		private void OnDisconnectMetamaskButtonClick()
		{
			AuthenticationService.DisconnectMetamask();
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnDisconnectMetamaskButtonClick);
		}
	}
}