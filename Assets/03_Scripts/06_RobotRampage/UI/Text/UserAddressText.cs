using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class UserAddressText: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Text _addressText;

		private void Awake()
		{
			_addressText = GetComponent<TMP_Text>();
			_addressText.text = UserService.UserAddress;
		}

		private void OnEnable()
		{
			TonAuthEvents.OnTonWalletConnected += OnTonWalletConnected;
			UserService.addressUpdated += OnTonWalletConnected;
		}

		private void OnDisable()
		{
			TonAuthEvents.OnTonWalletConnected -= OnTonWalletConnected;
			UserService.addressUpdated -= OnTonWalletConnected;
		}

		private void OnTonWalletConnected()
		{
			Debug.Log($"{nameof(ConnectTonButton)}::{nameof(OnTonWalletConnected)}");
			_addressText.text = UserService.UserAddress;
		}
	}
}