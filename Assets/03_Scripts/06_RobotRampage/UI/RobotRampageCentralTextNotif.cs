using System;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageCentralTextNotif: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _maxTime;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Text _text;

		[SerializeField]
		private float _timer;

		[SerializeField]
		private bool _showing;

		private void Awake()
		{
			_text = GetComponentInChildren<TMP_Text>();
		}

		private void OnEnable()
		{
			RobotRampageUIEvents.OnShowCentralNotification += OnShowCentralNotification;
		}

		private void OnDisable()
		{
			RobotRampageUIEvents.OnShowCentralNotification -= OnShowCentralNotification;
		}

		private void Update()
		{
			if (_showing){
				_timer -= Time.deltaTime;
				_text.color = new Color(1, 1, 1, _timer / _maxTime);
				if (_timer <= 0){
					_showing = false;
					_text.gameObject.Deactivate();
				}
			}
		}

		private void OnShowCentralNotification(string text)
		{
			_showing = true;
			_text.text = text;
			_text.color = new Color(1, 1, 1, 1);
			_text.gameObject.Activate();
			_timer = _maxTime;
		}
	}
}