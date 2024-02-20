#if SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard._02_BattleDash.State;
using PeanutDashboard.Shared.Logging;
#endif
using PeanutDashboard._02_BattleDash.Model;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
	public class BattleDashServerAreaController: NetworkBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _prefabBackground;
		
		[SerializeField]
		private BattleDashAreaType _battleDashAreaType;
		
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
			LoggerService.LogInfo($"{nameof(BattleDashServerAreaController)}::{nameof(InitialiseForStart)}");
			Vector3 position = start ? _battleDashAreaType.GetStartPosition() : _battleDashAreaType.GetSpawnPosition();
			_spawnedBackground = Instantiate(_prefabBackground, position, Quaternion.identity);
			_spawnedBackground.GetComponent<NetworkObject>().Spawn();
			_spawnedBackground.GetComponent<NetworkObject>().TrySetParent(this.transform);
			_direction = start ? _battleDashAreaType.GetStartDirection() : _battleDashAreaType.GetSpawnDirection();
			GetComponent<BattleDashServerAreaMonsterSpawner>().Initialise();
			_startingDistance = Mathf.Abs(position.x - _battleDashAreaType.GetEndSpawnPosition().x);
		}

		private void Update()
		{
			if (BattleDashServerGameState.isPaused){
				return;
			}
			_timeToStart -= NetworkManager.ServerTime.FixedDeltaTime;
			if (_destroyed || _timeToStart >= 0)
			{
				return;
			}
			_spawnedBackground.transform.Translate(_direction * (_battleDashAreaType.GetSpeed() * NetworkManager.ServerTime.FixedDeltaTime));
			if (!_newAreaSpawned)
			{
				float currentDistance = Mathf.Abs(_spawnedBackground.transform.localPosition.x - _battleDashAreaType.GetEndSpawnPosition().x);
				float perc = 1 - currentDistance / _startingDistance;
				BattleDashServerAreaEvents.RaiseAreaDistancePassedPercUpdatedEvent(perc);
				if (!_doorSpawned && perc >= 0.5f)
				{
					_doorSpawned = true;
					BattleDashServerAreaEvents.RaiseAreaSpawnDoorEvent();
				}
				else if (!_newAreaSpawned && perc >= 0.98f)
				{
					_newAreaSpawned = true;
					BattleDashServerAreaEvents.RaiseAreaSpawnNextAreaEvent();
				}
			}
			if (_spawnedBackground.transform.localPosition.x < _battleDashAreaType.GetDestroyPosition().x)
			{
				_destroyed = true;
				_spawnedBackground.GetComponent<BattleDashServerBackgroundController>().Remove();
				this.GetComponent<NetworkObject>().Despawn();
			}
		}
#endif
	}
}