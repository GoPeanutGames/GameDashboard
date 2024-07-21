using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class TonEvents
	{
		private static UnityAction<string> _tonWalletConnected;
		
		public static event UnityAction<string> OnTonWalletConnected
		{
			add => _tonWalletConnected += value;
			remove => _tonWalletConnected -= value;
		}
		
		public static void RaiseTonWalletConnectedEvent(string address)
		{
			if (_tonWalletConnected == null){
				LoggerService.LogWarning($"{nameof(RobotRampageAudioEvents)}::{nameof(RaiseTonWalletConnectedEvent)} raised, but nothing picked it up");
				return;
			}
			_tonWalletConnected.Invoke(address);
		}
	}
}