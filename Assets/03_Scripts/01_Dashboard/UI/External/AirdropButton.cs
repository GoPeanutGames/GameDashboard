using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Dashboard.UI.External
{
	public class AirdropButton: MonoBehaviour
	{
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnAirdropClick);
		}

		private void OnAirdropClick()
		{
			Application.OpenURL("https://peanutgames.com/airdrop");
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(OnAirdropClick);
		}
	}
}