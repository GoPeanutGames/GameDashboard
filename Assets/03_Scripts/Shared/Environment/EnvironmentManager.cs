using System.Collections.Generic;
using PeanutDashboard.Utils;
using UnityEngine;

namespace PeanutDashboard.Shared.Environment
{
	public class EnvironmentManager : MonoSingleton<EnvironmentManager>
	{
		[Header("Set in Inspector")]
		[SerializeField]
		private bool _isDev;
		
		[SerializeField]
		private string _devServerUrl;

		[SerializeField]
		private string  _prodServerUrl;

		[SerializeField]
		private List<string> _publicKey;

		public bool UseRSA => !_isDev;

		public bool LoggingEnabled => !_isDev;

		public string ServerUrl => _isDev ? _devServerUrl : _prodServerUrl;

		public List<string> PublicKey => _publicKey;
	}
}