using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class EffectSpawnController: MonoBehaviour
	{
		[SerializeField]
		private GameObject _bombasticExplosionPrefab;

		private void OnEnable()
		{
			EffectEvents.OnSpawnEffectAt += OnSpawnEffectAt;
		}

		private void OnDisable()
		{
			EffectEvents.OnSpawnEffectAt -= OnSpawnEffectAt;
		}

		private void OnSpawnEffectAt(EffectType effectType, Vector3 position)
		{
			switch (effectType){
				case EffectType.BombasticExplosion:
					Instantiate(_bombasticExplosionPrefab, position, Quaternion.identity);
					break;
			}
		}
	}
}