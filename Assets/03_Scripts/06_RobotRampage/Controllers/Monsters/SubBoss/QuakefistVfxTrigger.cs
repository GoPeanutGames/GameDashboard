using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage.SubBoss
{
	public class QuakefistVfxTrigger: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _vfxSpawnLocation;

		[SerializeField]
		private QuakefistAttackTrigger _quakefistAttackTrigger;
		
		public void TriggerVfx()
		{
			EffectEvents.RaiseSpawnEffectAt(EffectType.QuakefistVfx, _vfxSpawnLocation.transform.position);
			_quakefistAttackTrigger.DamagePlayer();
		}
	}
}