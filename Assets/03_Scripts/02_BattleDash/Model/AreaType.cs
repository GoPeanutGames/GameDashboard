using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Model
{[CreateAssetMenu(
		fileName = nameof(AreaType) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashModels + nameof(AreaType)
	)]
	public class AreaType: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _speed;
#if SERVER
		public float GetSpeed()
		{
			return _speed == 0 ? BattleDashConfig.DefaultAreaSpeed : _speed;
		}
#endif
	}
}