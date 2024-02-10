using PeanutDashboard.Init;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Picker;
using PeanutDashboard.Shared.User;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard.Shared.UI
{
	[RequireComponent(typeof(Button))]
	public class OpenGameButton: MonoBehaviour
	{
		[Header("Set In Inspector")]
		[SerializeField]
		private GameInfo _gameInfo;

		[Header("Debug Dynamic")]
		[SerializeField]
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnPlayButtonClick);
		}

		private void OnPlayButtonClick()
		{
			Debug.Log($"{nameof(OpenSceneButton)}::{nameof(OnPlayButtonClick)}");
			if (UserService.Instance.IsLoggedIn()){
				GameNetworkSyncService.AssignCurrentGameInfo(_gameInfo);
				SceneLoaderEvents.Instance.RaiseLoadAndOpenSceneEvent(GameNetworkSyncService.GetNetworkEntryPoint());
			}
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnPlayButtonClick);
		}
	}
}