using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard._02_BattleDash.State;
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
		private AreaType _areaType;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private GameObject _spawnedBackground;
		
		[SerializeField]
		private Vector3 _direction;
		
		[SerializeField]
		private float _startingDistance;

		[SerializeField]
		private bool _doorSpawned;

		[SerializeField]
		private bool _newAreaSpawned;
		
		[SerializeField]
		private bool _destroyed;
		
		[SerializeField]
		private float _timeToStart = 1f;

#if SERVER
		public void InitialiseForStart(bool start)
		{
			Debug.Log($"{nameof(ServerAreaController)}::{nameof(InitialiseForStart)}");
			Vector3 position = start ? _areaType.GetStartPosition() : _areaType.GetSpawnPosition();
			_spawnedBackground = Instantiate(_prefabBackground, position, Quaternion.identity);
			_spawnedBackground.GetComponent<NetworkObject>().Spawn();
			_spawnedBackground.GetComponent<NetworkObject>().TrySetParent(this.transform);
			_direction = start ? _areaType.GetStartDirection() : _areaType.GetSpawnDirection();
			GetComponent<ServerAreaMonsterSpawner>().Initialise();
			_startingDistance = Mathf.Abs(position.x - _areaType.GetEndSpawnPosition().x);
		}

		private void Update()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			_timeToStart -= NetworkManager.ServerTime.FixedDeltaTime;
			if (_destroyed || _timeToStart >= 0)
			{
				return;
			}
			_spawnedBackground.transform.Translate(_direction * (_areaType.GetSpeed() * NetworkManager.ServerTime.FixedDeltaTime));
			if (!_newAreaSpawned)
			{
				float currentDistance = Mathf.Abs(_spawnedBackground.transform.localPosition.x - _areaType.GetEndSpawnPosition().x);
				float perc = 1 - currentDistance / _startingDistance;
				ServerAreaEvents.RaiseAreaDistancePassedPercUpdatedEvent(perc);
				if (!_doorSpawned && perc >= 0.5f)
				{
					_doorSpawned = true;
					ServerAreaEvents.RaiseAreaSpawnDoorEvent();
				}
				else if (!_newAreaSpawned && perc >= 0.98f)
				{
					_newAreaSpawned = true;
					ServerAreaEvents.RaiseAreaSpawnNextAreaEvent();
				}
			}
			if (_spawnedBackground.transform.localPosition.x < _areaType.GetDestroyPosition().x)
			{
				_destroyed = true;
				_spawnedBackground.GetComponent<ServerBackgroundController>().Remove();
				this.GetComponent<NetworkObject>().Despawn();
			}
		}
#endif
	}
}