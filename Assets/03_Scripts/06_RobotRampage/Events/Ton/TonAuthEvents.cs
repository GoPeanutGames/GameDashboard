using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class TonAuthEvents
	{
		private static UnityAction _tonConnect;
		private static UnityAction _tonDisconnect;
		private static UnityAction<string> _tonWalletConnected;
		
		public static event UnityAction<string> OnTonWalletConnected
		{
			add => _tonWalletConnected += value;
			remove => _tonWalletConnected -= value;
		}
		
		public static event UnityAction OnTonConnect
		{
			add => _tonConnect += value;
			remove => _tonConnect -= value;
		}
		
		
		public static event UnityAction OnTonDisconnect
		{
			add => _tonDisconnect += value;
			remove => _tonDisconnect -= value;
		}
		
		public static void RaiseTonWalletConnectedEvent(string address)
		{
			if (_tonWalletConnected == null){
				LoggerService.LogWarning($"{nameof(TonAuthEvents)}::{nameof(RaiseTonWalletConnectedEvent)} raised, but nothing picked it up");
				return;
			}
			_tonWalletConnected.Invoke(address);
		}
		
		public static void RaiseTonConnectEvent()
		{
			if (_tonConnect == null){
				LoggerService.LogWarning($"{nameof(TonAuthEvents)}::{nameof(RaiseTonConnectEvent)} raised, but nothing picked it up");
				return;
			}
			_tonConnect.Invoke();
		}
		
		public static void RaiseTonDisconnectEvent()
		{
			if (_tonDisconnect== null){
				LoggerService.LogWarning($"{nameof(TonAuthEvents)}::{nameof(RaiseTonDisconnectEvent)} raised, but nothing picked it up");
				return;
			}
			_tonDisconnect.Invoke();
		}
	}
}