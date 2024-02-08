using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class ServerAreaController: NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _prefabBackground;
		
		[SerializeField]
		private Vector3 _startPositionAtStart;
		
		[SerializeField]
		private AreaType _areaType;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private GameObject _spawnedBackground;

#if SERVER
		public void InitialiseForStart()
		{
			Debug.Log($"{nameof(ServerAreaController)}::{nameof(InitialiseForStart)}");
			_spawnedBackground = Instantiate(_prefabBackground);
			_spawnedBackground.GetComponent<NetworkObject>().Spawn();
			_spawnedBackground.GetComponent<NetworkObject>().TrySetParent(this.transform);
			_spawnedBackground.transform.localPosition = _startPositionAtStart;
			GetComponent<ServerAreaMonsterSpawner>().Initialise(_spawnedBackground);
		}

		private void Update()
		{
			_spawnedBackground.transform.Translate(Vector3.left * (_areaType.GetSpeed() * NetworkManager.ServerTime.FixedDeltaTime));
		}
#endif
	}
}