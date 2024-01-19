using System;

namespace PeanutDashboard.Shared.Metamask.Model
{
	[Serializable]
	public class NativeCurrency
	{
		public string name;
		public string symbol;
		public int decimals;
	}
	
	[Serializable]
	public class Chain
	{
		public string chainId;
		public string[] rpcUrls;
		public string chainName;
		public NativeCurrency nativeCurrency;
		public string[] blockExplorerUrls;
	}

	public static class ChainDataReference
	{
		public const long MumbaiChainId = 80001;
		public const long PolygonChainId = 137;
		
		public static readonly Chain MumbaiChain = new ()
		{
			chainId = "0x13881", //80001
			rpcUrls = new[] { "https://rpc-mumbai.maticvigil.com" },
			chainName = "Matic Mumbai",
			nativeCurrency = new NativeCurrency()
			{
				name = "MATIC",
				symbol = "MATIC",
				decimals = 18
			},
			blockExplorerUrls = new[] { "https://mumbai.polygonscan.com/" }
		};
		
		public static readonly Chain PolygonChain = new ()
		{
			chainId = "0x89", //137
			rpcUrls = new[] { "https://polygon-rpc.com" },
			chainName = "Polygon Mainnet",
			nativeCurrency = new NativeCurrency()
			{
				name = "MATIC",
				symbol = "MATIC",
				decimals = 18
			},
			blockExplorerUrls = new[] { "https://polygonscan.com/" }
		};
	}
}