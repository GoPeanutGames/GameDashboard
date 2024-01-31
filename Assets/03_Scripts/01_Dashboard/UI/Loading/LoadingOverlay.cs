using System;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard.Dashboard.UI.Loading
{
	public class LoadingOverlay: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField] private TMP_Text _loadingText;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField] private GameObject _loadingOverlay;

		private void Awake()
		{
			_loadingOverlay = this.transform.GetChild(0).gameObject;
		}

		private void OnEnable()
		{
			LoadingEvents.Instance.ShowLoading += ShowLoading;
			LoadingEvents.Instance.UpdateLoading += UpdateLoading;
			LoadingEvents.Instance.HideLoading += HideLoading;
		}

		private void ShowLoading(string text)
		{
			LoggerService.LogInfo($"{nameof(LoadingOverlay)}::{nameof(ShowLoading)} - {text}");
			_loadingOverlay.gameObject.SetActive(true);
			_loadingText.text = text;
		}

		private void UpdateLoading(string text)
		{
			LoggerService.LogInfo($"{nameof(LoadingOverlay)}::{nameof(UpdateLoading)} - {text}");
			_loadingText.text = text;
		}

		private void HideLoading()
		{
			LoggerService.LogInfo($"{nameof(LoadingOverlay)}::{nameof(HideLoading)}");
			_loadingOverlay.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			LoadingEvents.Instance.ShowLoading -= ShowLoading;
			LoadingEvents.Instance.UpdateLoading -= UpdateLoading;
			LoadingEvents.Instance.HideLoading -= HideLoading;
		}
	}
}