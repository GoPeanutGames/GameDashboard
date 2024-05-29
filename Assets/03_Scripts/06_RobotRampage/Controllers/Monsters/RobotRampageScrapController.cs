using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageScrapController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _timeToDieMax;

		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _timeToDie;

		private void Start()
		{
			_timeToDie = _timeToDieMax;
		}

		private void Update()
		{
			_timeToDie -= Time.deltaTime;
			_spriteRenderer.color = new Color(1, 1, 1, _timeToDie / _timeToDieMax);
			if (_timeToDie <= 0){
				Destroy(this.gameObject);
			}
		}
	}
}