using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class EffectSpawnController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _bombasticExplosionPrefab;
		
		[SerializeField]
		private GameObject _swordixSwordVfxPrefab;

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
				case EffectType.SwordixSwordVfx:
					Instantiate(_swordixSwordVfxPrefab, position, Quaternion.identity);
					break;
			}
		}
	}
}