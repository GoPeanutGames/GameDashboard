using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeanutDashboard.Shared.Logging;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Matchmaker.Models;
using Unity.Services.Multiplay;
using UnityEngine;

namespace PeanutDashboard.UnityServer
{
	public class UnityServerStartUp : MonoBehaviour
	{
		public static event Action ClientInstance;
		
		private const string InternalServerIP = "0.0.0.0";
		private ushort _serverPort = 7777;
		private IMultiplayService _multiplayService;
		private const int MultiplayServiceTimeout = 20000;
		private string _allocationId;
		private MultiplayEventCallbacks _serverCallbacks;
		private IServerEvents _serverEvents;

		private async void Start()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(Start)}");
			bool server = false;
			string[] args = System.Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++){
				if (args[i] == "-dedicatedServer"){
					server = true;
				}
				if (args[i] == "-port" && (i + 1 < args.Length)){
					_serverPort = (ushort)int.Parse(args[i + 1]);
				}
			}
#if SERVER
			if (server){
				StartServer();
				await StartServerServices();
			}
#else
			ClientInstance?.Invoke();
#endif
		}

		private void StartServer()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - IP: {InternalServerIP} at port: {_serverPort}");
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(InternalServerIP, _serverPort);
			NetworkManager.Singleton.StartServer();
		}

		async Task StartServerServices()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)}");
			InitializationOptions options = new InitializationOptions();
			options.SetEnvironmentName("development");
			await UnityServices.InitializeAsync(options);
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)} - unity services state: {UnityServices.State}");
			try{
				_multiplayService = MultiplayService.Instance;
				await _multiplayService.StartServerQueryHandlerAsync(ConnectionApprovalHandler.MaxPlayers, "n/a", "n/a", "0", "n/a");
			}
			catch (Exception e){
				LoggerService.LogWarning($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)} - Something went wrong trying to set up the SQP service:\n{e}");
			}
			try{
				MatchmakingResults matchmakerPayload = await GetMatchmakerPayload(MultiplayServiceTimeout);
				if (matchmakerPayload != null){
					LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)} Got payload - {matchmakerPayload}");
				}
				else{
					LoggerService.LogWarning($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)} - Getting the matchmaker payload timed out, starting with defaults");
				}
			}
			catch (Exception e){
				LoggerService.LogWarning($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)} - Something went wrong trying to set up the allocation service:\n{e}");
			}
		}

		private async Task<MatchmakingResults> GetMatchmakerPayload(int timeout)
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(GetMatchmakerPayload)} or timeout {timeout}");
			Task<MatchmakingResults> matchmakerPayloadTask = SubscribeAndAwaitMatchamkerAllocation();
			if (await Task.WhenAny(matchmakerPayloadTask, Task.Delay(timeout)) == matchmakerPayloadTask){
				return matchmakerPayloadTask.Result;
			}
			return null;
		}

		private async Task<MatchmakingResults> SubscribeAndAwaitMatchamkerAllocation()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(SubscribeAndAwaitMatchamkerAllocation)}");
			if (_multiplayService == null){
				return null;
			}
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(SubscribeAndAwaitMatchamkerAllocation)} - subscribing to server callbacks and getting allocation id");
			_allocationId = null;
			_serverCallbacks = new MultiplayEventCallbacks();
			_serverCallbacks.Allocate += OnMultiplayAllocation;
			_serverEvents = await _multiplayService.SubscribeToServerEventsAsync(_serverCallbacks);
			_allocationId = await AwaitAllocationID();
			MatchmakingResults mmPayload = await GetMatchmakerAllocationPayloadAsync();
			return mmPayload;
		}

		private void OnMultiplayAllocation(MultiplayAllocation allocation)
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(OnMultiplayAllocation)} - Allocation id: {allocation.AllocationId}");
			if (string.IsNullOrEmpty(allocation.AllocationId)){
				return;
			}
			_allocationId = allocation.AllocationId;
		}

		private async Task<string> AwaitAllocationID()
		{
			ServerConfig serverConfig = _multiplayService.ServerConfig;
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(AwaitAllocationID)} - Awaiting Allocation. Server config is:\n" +
			                      $"-ServerID: {serverConfig.ServerId}\n" +
			                      $"-AllocationID:{serverConfig.AllocationId}\n" +
			                      $"-Port: {serverConfig.Port}\n" +
			                      $"-QPort: {serverConfig.QueryPort}\n" +
			                      $"-logs: {serverConfig.ServerLogDirectory}");
			while (string.IsNullOrEmpty(_allocationId)){
				string configId = serverConfig.AllocationId;
				if (!string.IsNullOrEmpty(configId) && string.IsNullOrEmpty(_allocationId)){
					_allocationId = configId;
					break;
				}
				await Task.Delay(100);
			}
			return _allocationId;
		}

		private async Task<MatchmakingResults> GetMatchmakerAllocationPayloadAsync()
		{
			LoggerService.LogWarning($"{nameof(UnityServerStartUp)}::{nameof(GetMatchmakerAllocationPayloadAsync)}");
			try{
				MatchmakingResults payloadAllocation = await MultiplayService.Instance.GetPayloadAllocationFromJsonAs<MatchmakingResults>();
				string modelAsJson = JsonConvert.SerializeObject(payloadAllocation, Formatting.Indented);
				LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(GetMatchmakerAllocationPayloadAsync)} - allocation:\n {modelAsJson}");
				return payloadAllocation;
			}
			catch (Exception e){
				LoggerService.LogWarning($"{nameof(UnityServerStartUp)}::{nameof(GetMatchmakerAllocationPayloadAsync)} - Something went wrong tying to get the Matchmaker payload:\n {e}");
			}
			return null;
		}

		private void Dispose()
		{
			_serverCallbacks.Allocate -= OnMultiplayAllocation;
			_serverEvents?.UnsubscribeAsync();
		}
	}
}