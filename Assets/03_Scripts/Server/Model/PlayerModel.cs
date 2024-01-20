using System;

namespace PeanutDashboard.Server.Data
{
	[Serializable]
	public class GetGeneralDataResponse
	{
		public string nickname;
		public int rank;
	}

	[Serializable]
	public class GetPlayerWalletResponse
	{
		public int gems;
		public int energy;
		public int bubbles;
	}
}