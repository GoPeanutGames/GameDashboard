using System;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageVirtualJoystick: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _visual;
		
		[SerializeField]
		private GameObject _centralVisual;
		
		[SerializeField]
		private float _movementRange;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private bool _active;
		
		[SerializeField]
		private bool _pcInput;
		
		[SerializeField]
		private bool _touchInput;

		private void Start()
		{
			_visual.Deactivate();
		}

		private bool CheckPcInput()
		{
			_pcInput = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
			return _pcInput;
		}

		private bool CheckPcInputUp()
		{
			return Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space);
		}

		private bool CheckTouchInput()
		{
			_touchInput = Input.touchCount > 0;
			return _touchInput;
		}

		private Vector3 GetInputPosition()
		{
			return _pcInput ? Input.mousePosition : Input.GetTouch(0).position;
		}
		
		private void Update()
		{
			if (!_active && (CheckPcInput() || CheckTouchInput()) && !EventSystem.current.IsPointerOverGameObject()){
				_visual.Activate();
				_visual.transform.position = GetInputPosition();
				_active = true;
			}else if (_active && CheckPcInputUp()){
				_active = false;
				_visual.Deactivate();
				RobotRampagePlayerEvents.RaiseMovementDirectionUpdated(Vector3.zero);
			}else if (_active){
				_centralVisual.transform.position = GetInputPosition();
				Vector3 newLocal = Vector3.ClampMagnitude(_centralVisual.transform.localPosition, _movementRange);
				_centralVisual.transform.localPosition = newLocal;
				float multiplier = _centralVisual.transform.localPosition.magnitude / _movementRange;
				Vector3 direction = _centralVisual.transform.localPosition.normalized * multiplier;
				RobotRampagePlayerEvents.RaiseMovementDirectionUpdated(direction);
			}
		}
	}
}