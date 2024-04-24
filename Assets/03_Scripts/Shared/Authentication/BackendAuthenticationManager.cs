using AOT;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Netcode;
using UnityEngine;

public class BackendAuthenticationManager : NetworkBehaviour
{
    private static AuthenticationData _authData;

#if !SERVER
    //retrieves authentication data (address, signature) stored in local browser cache
    [DllImport("__Internal")]
    private static extern void RequestAuthenticationInfo(Action<string> cbSuccess, Action cbFail);
    public static void RetrieveLocalAuthenticationData()
    {
        RequestAuthenticationInfo(OnAuthenticationDataSuccess, OnAuthenticationDataFail);
#if !UNITY_EDITOR
        RequestAuthenticationInfo(OnAuthenticationDataSuccess, OnAuthenticationDataFail);
#else
        _authData = new AuthenticationData()
        {
            address = "test_adress",
            signature = "test_signature"
        };
#endif
    }

    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void OnAuthenticationDataSuccess(string jsonData)
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(OnAuthenticationDataSuccess)} - {jsonData}");
        _authData = JsonUtility.FromJson<AuthenticationData>(jsonData);

        AuthenticationEvents.Instance.RaiseAuthenticationDataRetrievalSuccessEvent(_authData);
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
        RetrieveAuthenticationData_ServerRpc(JsonUtility.ToJson(_authData));
    }

    [ServerRpc(RequireOwnership = false)]
    private void RetrieveAuthenticationData_ServerRpc(string jsonData)
    {
        LoggerService.LogInfo($"[SERVER-RPC]{nameof(BackendAuthenticationManager)}::{nameof(RetrieveAuthenticationData_ServerRpc)}" + jsonData);
        _authData = JsonUtility.FromJson<AuthenticationData>(jsonData);
    }


}
