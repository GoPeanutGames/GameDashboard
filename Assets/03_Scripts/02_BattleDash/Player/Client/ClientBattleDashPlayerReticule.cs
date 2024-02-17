#if !SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.WebGL;
using System;
using PeanutDashboard._02_BattleDash.State;
using PeanutDashboard.Utils.Math;
#endif
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player
{
	public class BattleDashPlayerReticule : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RectTransform _reticule;
		
		[SerializeField]
		private Vector3 _offset;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Vector3 _currentVisualPosition;

#if !SERVER

		private void OnEnable()
		{
			ClientActionEvents.OnUpdatePlayerVisualPosition += OnUpdatePlayerVisualPosition;
			ClientActionEvents.OnMobilePlayerTouchShootPosition += OnMobileShootPositionChanged;
		}

		private void OnDisable()
		{
			ClientActionEvents.OnUpdatePlayerVisualPosition -= OnUpdatePlayerVisualPosition;
			ClientActionEvents.OnMobilePlayerTouchShootPosition -= OnMobileShootPositionChanged;
		}

		private void OnUpdatePlayerVisualPosition(Vector3 visualPosition)
		{
			_currentVisualPosition = visualPosition;
		}

		private void OnMobileShootPositionChanged(Vector2 position)
		{
			UpdateMobile(position);
		}
		
		private void Update()
		{
			if (ServerBattleDashGameState.isPaused){
				return;
			}
			if (!WebGLUtils.IsWebMobile){
				UpdateDesktop();
			}
		}

		private void UpdateDesktop()
		{
			_reticule.anchoredPosition = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            ProcessReticule();
		}

		private void UpdateMobile(Vector2 touchPosition)
		{
			_reticule.anchoredPosition =  touchPosition.ToVector3() - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            ProcessReticule();
		}

		private void ProcessReticule()
		{
			float minReticuleX = Camera.main.WorldToScreenPoint(_currentVisualPosition + _offset).x - Screen.width / 2f;
			_reticule.anchoredPosition = new Vector2(
				Mathf.Clamp(_reticule.anchoredPosition.x, minReticuleX, 10000f), _reticule.anchoredPosition.y);
			Vector2 target = (Vector2)Camera.main.ScreenToWorldPoint(_reticule.anchoredPosition + new Vector2(Screen.width / 2f,Screen.height / 2f));
			ClientActionEvents.RaiseUpdatePlayerAimEvent(target);
		}
#endif
	}
}