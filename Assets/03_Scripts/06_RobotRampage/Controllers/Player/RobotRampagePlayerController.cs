﻿using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerController : MonoBehaviour
	{
		public static Vector3 currentPosition;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private GameObject _playerImage;
		
		[SerializeField]
		private Vector3 _currentDirection = Vector3.zero;

		private void OnEnable()
		{
			RobotRampagePlayerEvents.OnPlayerKilled += OnPlayerKilled;
			RobotRampagePlayerEvents.OnMovementDirectionUpdated += OnUpdateMovementDirection;
		}

		private void OnDisable()
		{
			RobotRampagePlayerEvents.OnPlayerKilled -= OnPlayerKilled;
			RobotRampagePlayerEvents.OnMovementDirectionUpdated -= OnUpdateMovementDirection;
		}

		private void Start()
		{
			Camera.main.transform.SetParent(this.transform);
		}

		private void Update()
		{
			if (_currentDirection != Vector3.zero){
				this.transform.Translate(_currentDirection * Time.deltaTime * RobotRampageCharacterStatsService.GetSpeed());
				float angle = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg;
				_playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
				currentPosition = this.transform.position;
			}
		}

		private void OnUpdateMovementDirection(Vector3 direction)
		{
			_currentDirection = direction;
		}
		
		private void OnPlayerKilled()
		{
			Camera.main.transform.SetParent(null);
			Destroy(this.gameObject);
		}
	}
}