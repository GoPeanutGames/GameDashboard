using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageParticleSortOrderSetter : MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageSortOrderType _robotRampageSortOrderType;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private ParticleSystemRenderer _particleSystemRenderer;

		private void Awake()
		{
			_particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
			_particleSystemRenderer.sortingOrder = RobotRampageSortOrderService.GetSortOrderForType(_robotRampageSortOrderType);
		}
	}
}