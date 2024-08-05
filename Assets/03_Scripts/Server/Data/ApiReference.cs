using System;
using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;

namespace PeanutDashboard.Server
{
	public enum TonAuthApi
	{
		GetVerifyProof,
		VerifyProof
	}

	public static class ApiReference
	{
        private static readonly Dictionary<TonAuthApi, string> TonAuthApiMap = new()
        {
	        { TonAuthApi.GetVerifyProof , "/ton/get-payload"},
            { TonAuthApi.VerifyProof, "/ton/verify-proof" },
        };
        
        

        public static string GetApi<T>(T api) where T : struct, IConvertible
		{
			Type apiType = typeof(T);
			if (typeof(TonAuthApi) == apiType) 
			{
				TonAuthApi playerApi = (TonAuthApi)Convert.ChangeType(api, typeof(TonAuthApi));
				return TonAuthApiMap[playerApi];
			}


            LoggerService.LogError($"{nameof(ApiReference)}::{nameof(GetApi)} - type {apiType} is not supported");
			return "";
		}
	}
}