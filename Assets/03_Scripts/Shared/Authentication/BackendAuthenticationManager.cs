using AOT;
using Org.BouncyCastle.Asn1.Ocsp;
using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay.Models;
using UnityEngine;

public class BackendAuthenticationManager : NetworkBehaviour
{
    private static AuthenticationData authenticationData;

    private static GameplaySession gameplaySession;

    private static bool loggedIn = false;

#if !SERVER
    //retrieves authentication data (address, signature) stored in local browser cache
    [DllImport("__Internal")]
    private static extern void RequestAuthenticationInfo(Action<string> cbSuccess, Action cbFail);
    public static void RetrieveLocalAuthenticationData()
    {
#if !UNITY_EDITOR
        RequestAuthenticationInfo(OnAuthenticationDataSuccess, OnAuthenticationDataFail);
#else
        authenticationData = new AuthenticationData()
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
        authenticationData = JsonUtility.FromJson<AuthenticationData>(jsonData);

        AuthenticationEvents.Instance.RaiseAuthenticationDataRetrievalSuccessEvent(authenticationData);
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
        RetrieveAuthenticationData_ServerRpc(JsonUtility.ToJson(authenticationData));
    }

    [ServerRpc(RequireOwnership = false)]
    private void RetrieveAuthenticationData_ServerRpc(string jsonData)
    {
        LoggerService.LogInfo($"[SERVER-RPC]{nameof(BackendAuthenticationManager)}::{nameof(RetrieveAuthenticationData_ServerRpc)}" + jsonData);
        authenticationData = JsonUtility.FromJson<AuthenticationData>(jsonData);
        LoginToBackend();
    }

    public static void SubmitGameEnd(bool _won)
    {
        gameplaySession.EndSession(_won ? true : false, 0);
    }

    private void LoginToBackend()
    {
        CheckWeb3LoginRequest request = new CheckWeb3LoginRequest()
        {
            address = authenticationData.address,
            signature = authenticationData.signature
        };
        string requestJson = JsonUtility.ToJson(request);
        ServerService.PostDataToServer(AuthenticationApi.Web3LoginCheck, requestJson, CheckWeb3LoginCallback, CheckWeb3LoginFailed);
    }

    private static void CheckWeb3LoginFailed(string result)
    {
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(CheckWeb3LoginFailed)} - Web3 Login check failed : {result}");
    }

    private static void CheckWeb3LoginCallback(string result)
    {
        CheckWeb3LoginResponse response = JsonUtility.FromJson<CheckWeb3LoginResponse>(result);
        LoggerService.LogInfo($"{nameof(BackendAuthenticationManager)}::{nameof(CheckWeb3LoginCallback)} - Web3 Login check: {response.status}");
        if (response.status)
        {
            _ = SignInToUnity();
            loggedIn = true;
            gameplaySession = new GameplaySession(authenticationData, "BATTLE_DASH", "FREE");
            gameplaySession.StartSession();
        }
    }

    private static async Task SignInToUnity()
    {
        InitializationOptions options = new();
        options.SetEnvironmentName(EnvironmentManager.Instance.GetUnityEnvironmentName());
        UnityServices.ExternalUserId = authenticationData.address;
        await UnityServices.InitializeAsync(options);
        await Unity.Services.Authentication.AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
