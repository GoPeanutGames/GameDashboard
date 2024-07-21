using System;
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
		}

		private void OnEnable()
		{
			TonEvents.OnTonWalletConnected += OnTonWalletConnected;
		}

		private void OnDisable()
		{
			TonEvents.OnTonWalletConnected -= OnTonWalletConnected;
		}

		private void OnTonWalletConnected(string address)
		{
			Debug.Log($"{nameof(ConnectTonButton)}::{nameof(OnTonWalletConnected)}");
			_addressText.text = address;
		}
	}
}