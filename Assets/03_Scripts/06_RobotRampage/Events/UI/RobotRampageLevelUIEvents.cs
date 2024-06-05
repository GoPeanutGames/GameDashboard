using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageLevelUIEvents
	{
		
		private static UnityAction<float, float> _updateUIExp;
		private static UnityAction<int> _updateUILevel;

		public static event UnityAction<float,float> OnUpdateUIExp
		{
			add => _updateUIExp += value;
			remove => _updateUIExp -= value;
		}
		
		public static event UnityAction<int> OnUpdateUILevel
		{
			add => _updateUILevel += value;
			remove => _updateUILevel -= value;
		}
		
		public static void RaiseUpdateUIExpEvent(float currentExp, float maxExp)
		{
			if (_updateUIExp == null){
				LoggerService.LogWarning($"{nameof(RobotRampageLevelUIEvents)}::{nameof(RaiseUpdateUIExpEvent)} raised, but nothing picked it up");
				return;
			}
			_updateUIExp.Invoke(currentExp, maxExp);
		}
		
		public static void RaiseUpdateUILevelEvent(int level)
		{
			if (_updateUILevel == null){
				LoggerService.LogWarning($"{nameof(RobotRampageLevelUIEvents)}::{nameof(RaiseUpdateUILevelEvent)} raised, but nothing picked it up");
				return;
			}
			_updateUILevel.Invoke(level);
		}
	}
}