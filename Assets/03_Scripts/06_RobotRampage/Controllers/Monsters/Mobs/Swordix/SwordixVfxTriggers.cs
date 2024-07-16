using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class SwordixVfxTriggers: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _vfxSpawnPoint1;
		
		[SerializeField]
		private GameObject _vfxSpawnPoint2;
		
		[SerializeField]
		private GameObject _vfxSpawnPoint3;

		public void TriggerVfxOne()
		{
			EffectEvents.RaiseSpawnEffectAt(EffectType.SwordixSwordVfx, _vfxSpawnPoint1.transform.position);
		}

		public void TriggerVfxTwo()
		{
			EffectEvents.RaiseSpawnEffectAt(EffectType.SwordixSwordVfx, _vfxSpawnPoint2.transform.position);
		}

		public void TriggerVfxThree()
		{
			EffectEvents.RaiseSpawnEffectAt(EffectType.SwordixSwordVfx, _vfxSpawnPoint3.transform.position);
		}
	}
}