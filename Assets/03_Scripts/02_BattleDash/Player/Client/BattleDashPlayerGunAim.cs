using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using Unity.Netcode;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Player.Client
{
	public class BattleDashPlayerGunAim: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _ikTarget;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Vector2 _targetPosition;
		
		private void OnEnable()
		{
			ClientActionEvents.OnUpdatePlayerAim += OnPlayerAimUpdated;
		}

		private void OnPlayerAimUpdated(Vector2 target)
		{
			_targetPosition = target;
		}

		private void OnDisable()
		{
			ClientActionEvents.OnUpdatePlayerAim -= OnPlayerAimUpdated;
		}
		
		private void Update()
		{
			_ikTarget.transform.position = _targetPosition;
		}
	}
}