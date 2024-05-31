using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerExperience: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private CircleCollider2D _circleCollider2D;

		private void Awake()
		{
			_circleCollider2D = GetComponent<CircleCollider2D>();
			_circleCollider2D.radius = RobotRampageCharacterStatsService.GetAttractionRange();
		}

		private void OnEnable()
		{
			RobotRampagePlayerEvents.OnAddPlayerExperience += OnAddPlayerExperience;
		}

		private void OnDisable()
		{
			RobotRampagePlayerEvents.OnAddPlayerExperience -= OnAddPlayerExperience;
		}

		private void OnAddPlayerExperience(float exp)
		{
			//TODO: add exp
			// Debug.LogError($"ADD EXP: {exp}");
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			RobotRampageExpController expController = other.GetComponent<RobotRampageExpController>();
			if (expController != null){
				expController.StartAttraction();
			}
		}
	}
}