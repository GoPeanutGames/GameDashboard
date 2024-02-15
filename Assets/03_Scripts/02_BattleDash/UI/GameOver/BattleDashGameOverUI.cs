﻿using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.GameOver
{
	public class BattleDashGameOverUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _gameOverUI;

		private void OnEnable()
		{
			BattleDashClientUIEvents.OnShowGameOver += OnShowGameOver;
		}

		private void OnDisable()
		{
			BattleDashClientUIEvents.OnShowGameOver -= OnShowGameOver;
		}

		private void OnShowGameOver()
		{
			_gameOverUI.Activate();
		}
	}
}