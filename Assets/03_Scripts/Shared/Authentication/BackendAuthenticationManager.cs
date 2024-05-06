using AOT;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using System;
using System.Runtime.InteropServices;
using Unity.Netcode;
using UnityEngine;

public class BackendAuthenticationManager : NetworkBehaviour
{

#if !SERVER
    public static void RetrieveLocalAuthenticationData()
    {
    }

    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void OnAuthenticationDataSuccess(string jsonData)
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(OnAuthenticationDataSuccess)} - {jsonData}");
    }

    [MonoPInvokeCallback(typeof(Action))]
    private static void OnAuthenticationDataFail()
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(OnAuthenticationDataFail)}");
        AuthenticationEvents.Instance.RaiseAuthenticationDataRetrievalFailEvent();
    }
#endif

        private void OnEnable()
    {
#if SERVER
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(OnEnable)}");
        UnityServerStartUp.ServerInstance += SetupServerEvents;
#else
        RetrieveLocalAuthenticationData();
#endif
    }


#if SERVER
    private void SetupServerEvents()
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(SetupServerEvents)}");
        NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected;
    }

    private void ClientConnected(ulong id)
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(ClientConnected)}");
        RetrieveAuthenticationData_ClientRpc();

    }
#endif

    [ClientRpc]
    private void RetrieveAuthenticationData_ClientRpc()
    {
        LoggerService.LogInfo($"[CLIENT-RPC]{nameof(BackendAuthenticationManager)}::{nameof(RetrieveAuthenticationData_ClientRpc)}");

    }

    [ServerRpc(RequireOwnership = false)]
    private void RetrieveAuthenticationData_ServerRpc(string jsonData)
    {
        LoggerService.LogInfo($"[SERVER-RPC]{nameof(BackendAuthenticationManager)}::{nameof(RetrieveAuthenticationData_ServerRpc)}" + jsonData);

    }
}
