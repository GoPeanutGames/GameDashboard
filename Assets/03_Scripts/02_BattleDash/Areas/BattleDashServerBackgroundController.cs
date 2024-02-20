#if SERVER
using PeanutDashboard._02_BattleDash.Events;
using Unity.Netcode;
#endif
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Areas
{
    public class BattleDashServerBackgroundController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _doorPrefab;
        
        [SerializeField]
        private Vector3 _doorSpawnLocation;

        [SerializeField]
        private Vector3 _doorScale;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private GameObject _spawnedDoor;

#if SERVER
        private void Awake()
        {
            BattleDashServerAreaEvents.AreaSpawnDoor += OnAreaSpawnDoor;
        }

        private void OnAreaSpawnDoor()
        {
            Debug.Log($"{nameof(BattleDashServerBackgroundController)}::{nameof(OnAreaSpawnDoor)}");
            _spawnedDoor = Instantiate(_doorPrefab);
            _spawnedDoor.transform.localScale = _doorScale;
            _spawnedDoor.transform.localPosition = _doorSpawnLocation;
            _spawnedDoor.GetComponent<NetworkObject>().Spawn();
            _spawnedDoor.GetComponent<NetworkObject>().TrySetParent(this.gameObject, false);
        }
        
        private void OnDestroy()
        {
            BattleDashServerAreaEvents.AreaSpawnDoor -= OnAreaSpawnDoor;
        }

        public void Remove()
        {
            _spawnedDoor.GetComponent<NetworkObject>().Despawn();
            this.GetComponent<NetworkObject>().Despawn();
        }
#endif
    }
}