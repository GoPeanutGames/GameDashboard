using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.Model
{
	[CreateAssetMenu(
		fileName = nameof(BattleDashAreaType) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.BattleDashModels + nameof(BattleDashAreaType)
	)]
	public class BattleDashAreaType: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _speed;
		
		[SerializeField]
		private Vector3 _startPosition;
		
		[SerializeField]
		private Vector3 _spawnPosition;

		[SerializeField]
		private Vector3 _destroyPosition;
		
		[SerializeField]
		private Vector3 _endMonsterSpawnPosition;
		
#if SERVER
		public float GetSpeed()
		{
			return _speed == 0 ? BattleDashConfig.DefaultAreaSpeed : _speed;
		}

		public Vector3 GetStartDirection()
		{
			return (_destroyPosition - _startPosition).normalized;
		}

		public Vector3 GetSpawnDirection()
		{
			return (_destroyPosition - _spawnPosition).normalized;
		}

		public Vector3 GetStartPosition()
		{
			return _startPosition;
		}
		
		public Vector3 GetSpawnPosition()
		{
			return _spawnPosition;
		}
		
		public Vector3 GetDestroyPosition()
		{
			return _destroyPosition;
		}
		
		public Vector3 GetEndSpawnPosition()
		{
			return _endMonsterSpawnPosition;
		}
#endif
	}
}