using Unity.Netcode;



public class PlayerController : NetworkBehaviour
{
    // private void OnEnable()
    // {
    //     ServerSyncEvents.SpawnPlayerPrefab += SpawnPlayer;
    // }
    //
    // private async void SpawnPlayer(string guid)
    // {
    //     AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(guid);
    //     GameObject prefab = await handle.Task;
    //     Addressables.Release(handle);
    //     GameObject playerInstance = Instantiate(prefab);
    //     playerInstance.GetComponent<NetworkObject>().Spawn();
    // }
    //
    // private void OnDisable()
    // {
    //     ServerSyncEvents.SpawnPlayerPrefab -= SpawnPlayer;
    // }
}
