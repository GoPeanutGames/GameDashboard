using System;

namespace PeanutDashboard.Server.Data
{
	[Serializable]
	public class GetGeneralDataResponse
	{
		public string nickname;
		public int rank;
		public string code;
	}

	[Serializable]
	public class GetPlayerWalletResponse
	{
		public int gems;
		public int energy;
		public int bubbles;
	}
}