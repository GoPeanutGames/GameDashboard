using PeanutDashboard.UnityServer.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Server
{
	public class ServerBattleDashPlayerSpawner : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private string _playerPrefabKey;

		private void Awake()
		{
			if (!NetworkManager.Singleton.IsServer){
				Destroy(this.gameObject);
			}
		}

		private void OnEnable()
		{
			ServerSyncEvents.StartGameEvent += SpawnPlayer;
		}

		private void SpawnPlayer()
		{
			ServerSyncEvents.RaiseSpawnPlayerPrefabEvent(_playerPrefabKey);
		}

		private void OnDisable()
		{
			ServerSyncEvents.StartGameEvent -= SpawnPlayer;
		}
	}
}