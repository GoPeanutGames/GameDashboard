using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
	public class ConnectionApprovalHandler: MonoBehaviour
	{
		public ushort maxPlayers = 1;

		private void Start()
		{
			NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
		}

		private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
		{
			response.Approved = true;
			response.CreatePlayerObject = true;
			response.PlayerPrefabHash = null;
			if (NetworkManager.Singleton.ConnectedClients.Count >= maxPlayers){
				response.Approved = false;
				response.Reason = "Server is Full";
			}
			response.Pending = false;
		}
	}
}