using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashClientPlayerGunAim: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _ikTarget;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Vector2 _targetPosition;
		
		private void OnEnable()
		{
			BattleDashClientActionEvents.OnUpdatePlayerTarget += OnPlayerTargetUpdated;
		}

		private void OnPlayerTargetUpdated(Vector2 target)
		{
			_targetPosition = target;
		}

		private void OnDisable()
		{
			BattleDashClientActionEvents.OnUpdatePlayerTarget -= OnPlayerTargetUpdated;
		}
		
		private void Update()
		{
			_ikTarget.transform.position = _targetPosition;
		}
	}
}