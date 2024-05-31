using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageExpController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _expGain;
		
		[SerializeField]
		private bool _attract;
		
		[SerializeField]
		private float _speed;

		private void Update()
		{
			if (!_attract){
				return;
			}
			_speed += Time.deltaTime;
			Vector3 dir = RobotRampagePlayerController.currentPosition - this.transform.position;
			this.transform.Translate(dir.normalized * _speed * Time.deltaTime);
			if (Vector3.Distance(RobotRampagePlayerController.currentPosition, this.transform.position) < 0.2f){
				RobotRampagePlayerEvents.RaiseAddPlayerExperienceEvent(_expGain);
				Destroy(this.gameObject);
			}
		}

		public void Setup(Sprite sprite, float exp)
		{
			_spriteRenderer.sprite = sprite;
			_expGain = exp;
		}

		public void StartAttraction()
		{
			_attract = true;
		}
	}
}