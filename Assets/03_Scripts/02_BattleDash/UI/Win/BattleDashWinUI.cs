﻿using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.Win
{
	public class BattleDashWinUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _wonUI;
		
		[SerializeField]
		private AudioClip _audioClip;

		private void OnEnable()
		{
			BattleDashClientUIEvents.OnShowWon += OnShowGameOver;
		}

		private void OnDisable()
		{
			BattleDashClientUIEvents.OnShowWon -= OnShowGameOver;
		}

		private void OnShowGameOver()
		{
			_wonUI.Activate();
			BattleDashAudioEvents.RaisePlaySfxEvent(_audioClip,1);
		}
	}
}