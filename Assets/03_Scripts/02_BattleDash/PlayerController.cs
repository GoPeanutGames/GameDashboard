using PeanutDashboard.UnityServer.Events;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerController : NetworkBehaviour
{
    private void OnEnable()
    {
        ServerSyncEvents.SpawnPlayerPrefab += SpawnPlayer;
    }

    private async void SpawnPlayer(string prefabKey)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabKey, this.transform);
        GameObject playerInstance = await handle.Task;
        Addressables.Release(handle);
        playerInstance.GetComponent<NetworkObject>().Spawn();
    }

    private void OnDisable()
    {
        ServerSyncEvents.SpawnPlayerPrefab -= SpawnPlayer;
    }
}
