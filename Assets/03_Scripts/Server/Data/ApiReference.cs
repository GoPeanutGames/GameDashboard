﻿using System;
using System.Collections.Generic;
using PeanutDashboard.Shared.Logging;

namespace PeanutDashboard.Server.Data
{
	public enum AuthenticationApi
	{
		GetLoginSchema,
		Web3LoginCheck
	}

	public enum PlayerApi
	{
		GetGeneralData,
		GetWallet
	}

	public static class ApiReference
	{
		private static readonly Dictionary<AuthenticationApi, string> AuthApiMap = new()
		{
			{ AuthenticationApi.GetLoginSchema, "/auth/login-schema/" },
			{ AuthenticationApi.Web3LoginCheck, "/auth/web3-login" },
		};

		private static readonly Dictionary<PlayerApi, string> PlayerApiMap = new()
		{
			{ PlayerApi.GetGeneralData, "/player/me/" },
			{ PlayerApi.GetWallet, "/player/wallet/" },
		};

		public static string GetApi<T>(T api) where T : struct, IConvertible
		{
			Type apiType = typeof(T);
			if (typeof(PlayerApi) == apiType){
				PlayerApi playerApi = (PlayerApi)Convert.ChangeType(api, typeof(PlayerApi));
				return PlayerApiMap[playerApi];
			}
			if (typeof(AuthenticationApi) == apiType){
				AuthenticationApi authenticationApi = (AuthenticationApi)Convert.ChangeType(api, typeof(AuthenticationApi));
				return AuthApiMap[authenticationApi];
			}
			LoggerService.LogError($"{nameof(ApiReference)}::{nameof(GetApi)} - type {apiType} is not supported");
			return "";
		}
	}
}