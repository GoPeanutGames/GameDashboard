﻿using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageGoHomeButton: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;
        
		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnGoHomeButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnGoHomeButtonClick);
		}

		private void OnGoHomeButtonClick()
		{
			SceneManager.LoadScene(0);
		}
	}
}