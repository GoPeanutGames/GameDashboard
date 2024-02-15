﻿using System.Collections;
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._02_BattleDash.UI
{
	public class OpenTooltipsButton: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private AudioClip _audioClip;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;
		

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnCloseTooltipsButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnCloseTooltipsButtonClick);
		}

		private void OnCloseTooltipsButtonClick()
		{
			BattleDashClientUIEvents.RaiseShowTooltipsEvent(false);
			StartCoroutine(PlaySfx());
		}

		private IEnumerator PlaySfx()
		{
			yield return new WaitForSecondsRealtime(0.3f);
			BattleDashAudioEvents.RaisePlaySfxEvent(_audioClip, 1);
		}
	}
}