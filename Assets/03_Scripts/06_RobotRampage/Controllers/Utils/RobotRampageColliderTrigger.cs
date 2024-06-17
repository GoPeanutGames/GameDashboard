using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageColliderTrigger: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private UnityAction<Collider2D> _onTriggerEnter;
		
		[SerializeField]
		private UnityAction<Collider2D> _onTriggerExit;

		public void AddListenerOnEnter(UnityAction<Collider2D> action)
		{
			_onTriggerEnter += action;
		}
		
		public void AddListenerOnExit(UnityAction<Collider2D> action)
		{
			_onTriggerExit += action;
		}

		public void RemoveListenerOnEnter(UnityAction<Collider2D> action)
		{
			_onTriggerEnter -= action;
		}
		
		public void RemoveListenerOnExit(UnityAction<Collider2D> action)
		{
			_onTriggerExit -= action;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			_onTriggerEnter.Invoke(other);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			_onTriggerExit.Invoke(other);
		}
	}
}