using System;
using PeanutDashboard.Shared.Logging;
using PeanutDashboard.UnityServer.Config;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard.Dashboard.UI.Region
{
	public class RegionDropdown: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Dropdown _dropdown;

		private void Awake()
		{
			_dropdown = GetComponent<TMP_Dropdown>();
		}

		private void OnEnable()
		{
			_dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
		}

		private void OnDisable()
		{
			_dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
		}

		private void OnDropdownValueChanged(int index)
		{
			LoggerService.LogInfo($"{nameof(RegionDropdown)}::{nameof(OnDropdownValueChanged)} - {_dropdown.options[index].text}");
			string value = _dropdown.options[index].text;
			ServerRegionConfig.region = value;
		}
	}
}