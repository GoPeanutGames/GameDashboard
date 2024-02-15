﻿using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
	public static class BattleDashLoadingEvents
	{
		private static UnityAction<string> _updateLoadingText;
		private static UnityAction _closeLoading;
		
		public static event UnityAction<string> OnUpdateLoadingText
		{
			add => _updateLoadingText += value;
			remove => _updateLoadingText -= value;
		}
		
		public static event UnityAction OnCloseLoading
		{
			add => _closeLoading += value;
			remove => _closeLoading -= value;
		}

		public static void RaiseUpdateLoadingTextEvent(string text)
		{
			if (_updateLoadingText == null){
				LoggerService.LogWarning($"{nameof(BattleDashLoadingEvents)}::{nameof(RaiseUpdateLoadingTextEvent)} raised, but nothing picked it up");
				return;
			}
			_updateLoadingText.Invoke(text);
		}

		public static void RaiseCloseLoadingEvent()
		{
			if (_closeLoading == null){
				LoggerService.LogWarning($"{nameof(BattleDashLoadingEvents)}::{nameof(RaiseCloseLoadingEvent)} raised, but nothing picked it up");
				return;
			}
			_closeLoading.Invoke();
		}
	}
}