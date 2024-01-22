using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using UnityEngine;

namespace PeanutDashboard.BattleDash
{
	public class BattleDashMenuController : MonoBehaviour
	{
		private void OnEnable()
		{
			SceneLoaderEvents.Instance.LoadAndOpenScene += OnStartGame;
		}

		private void OnStartGame(SceneInfo sceneInfo)
		{
			LoggerService.LogInfo($"{nameof(BattleDashMenuController)}::{nameof(OnStartGame)}");
			SceneLoaderService.Instance.LoadAndOpenScene(sceneInfo);
		}

		private void OnDisable()
		{
			SceneLoaderEvents.Instance.LoadAndOpenScene -= OnStartGame;
		}
	}
}