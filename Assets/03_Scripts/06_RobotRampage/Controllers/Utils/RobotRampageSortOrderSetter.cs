using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageSortOrderSetter: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageSortOrderType _robotRampageSortOrderType;

		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_spriteRenderer.sortingOrder = RobotRampageSortOrderService.GetSortOrderForType(_robotRampageSortOrderType);
		}
	}
}