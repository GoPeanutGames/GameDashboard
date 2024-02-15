using System;
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
			string value = _dropdown.options[0].text;
			ServerRegionConfig.region = value;
		}
	}
}