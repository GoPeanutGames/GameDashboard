using System;
using PeanutDashboard.Init;
using PeanutDashboard.Shared.Config;
using PeanutDashboard.Shared.Environment;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Shared.Picker;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System.Threading.Tasks;
using PeanutDashboard.UnityServer.Events;
#if SERVER
using System.Collections;
using Newtonsoft.Json;
using PeanutDashboard.UnityServer.Events;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Matchmaker.Models;
using Unity.Services.Multiplay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
#endif
using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
	public class UnityServerStartUp : MonoBehaviour
	{
		public static event Action ClientInstance;
		public static event Action ServerInstance; 
		
		private GameConfig _gameConfig;
		private const string InternalServerIP = "0.0.0.0";
		private ushort _serverPort = 7777;
		private const int MultiplayServiceTimeout = 20000;
		private string _allocationId;
		private string _lobbyId;
#if SERVER
		private IMultiplayService _multiplayService;
		private MultiplayEventCallbacks _serverCallbacks;
		private IServerEvents _serverEvents;
		private IServerQueryHandler _serverQueryHandler;
		private float _timeout = 60f;
#endif

		private void OnEnable()
		{
			ServerEvents.StartServer += OnStartServer;
		}

		private void OnDisable()
		{
			ServerEvents.StartServer -= OnStartServer;
		}

		private async void OnStartServer(GameInfo gameInfo)
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(OnStartServer)}");
			_gameConfig = EnvironmentManager.Instance.GetGameConfig();
			GameNetworkSyncService.AssignCurrentGameInfo(gameInfo);
			await InitialiseAuth();
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
				ServerInstance?.Invoke();
				await StartServer();
				await StartServerServices();
			}
#else
			ClientInstance?.Invoke();
#endif
		}

		private async Task InitialiseAuth()
		{
			InitializationOptions options = new InitializationOptions();
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(InitialiseAuth)} - config: {_gameConfig.currentEnvironmentModel.unityEnvironmentName}");
			options.SetEnvironmentName(_gameConfig.currentEnvironmentModel.unityEnvironmentName);
			await UnityServices.InitializeAsync(options);
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(InitialiseAuth)} - unity services state: {UnityServices.State}");
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(InitialiseAuth)} - unity services sign in: {AuthenticationService.Instance.IsSignedIn}");
		}
		
#if SERVER
		private async Task StartServer()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - IP: {InternalServerIP} at port: {_serverPort}");
			Allocation allocation = await RelayService.Instance.CreateAllocationAsync((int)ConnectionApprovalHandler.MaxPlayers);
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "wss"));
			string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - unity services relay join - {joinCode}");

			try{
				LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - creating lobby");
				CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions();
				createLobbyOptions.IsPrivate = false;
				createLobbyOptions.Data = new Dictionary<string, DataObject>()
				{
					{
						"joinCode", new DataObject(
							DataObject.VisibilityOptions.Member, joinCode)
					}
				};
				Lobby lobby = await Lobbies.Instance.CreateLobbyAsync("n/a", 2, createLobbyOptions);
				LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - lobby created");
				_lobbyId = lobby.Id;
				LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServer)} - lobby created - {lobby.Id}");
				StartCoroutine(HeartbeatLobbyCoroutine(15));
			}
			catch (LobbyServiceException e){
				Debug.LogError($"{nameof(UnityServerStartUp)}::{nameof(StartServer)}:: {e}");
			}
			NetworkManager.Singleton.StartServer();
			ServerEvents.ShutDownServer += ShutdownServer;
		}

		private IEnumerator HeartbeatLobbyCoroutine(float waitTimeSeconds)
		{
			var delay = new WaitForSeconds(waitTimeSeconds);
			while (true){
				Lobbies.Instance.SendHeartbeatPingAsync(_lobbyId);
				yield return delay;
			}
		}

		async Task StartServerServices()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(StartServerServices)}");
			try{
				_multiplayService = MultiplayService.Instance;
				_serverQueryHandler = await _multiplayService.StartServerQueryHandlerAsync(ConnectionApprovalHandler.MaxPlayers, "n/a", "n/a", "0", "n/a");
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

		private void Update()
		{
			if (_serverQueryHandler != null){
				_serverQueryHandler.UpdateServerCheck();
			}
			_timeout -= Time.unscaledDeltaTime;
			if (_timeout<=0){
				_timeout = 60f;
				if (NetworkManager.Singleton.ConnectedClients.Count == 0){
					LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(Update)} - no player for 60 sec, shutting down");
					ShutdownServer();
				}
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
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(GetMatchmakerAllocationPayloadAsync)}");
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

		private void ShutdownServer()
		{
			LoggerService.LogInfo($"{nameof(UnityServerStartUp)}::{nameof(ShutdownServer)}");
			Application.Quit(1);
		}

#endif
	}
}