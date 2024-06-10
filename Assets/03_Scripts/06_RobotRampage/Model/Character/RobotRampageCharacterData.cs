using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[CreateAssetMenu(
		fileName = nameof(RobotRampageCharacterData) + ExtensionNames.DotAsset,
		menuName = PeanutDashboardMenuNames.RobotRampageModels + nameof(RobotRampageCharacterData)
	)]
	public class RobotRampageCharacterData: ScriptableObject
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageCharacterStats _characterStats;

		[SerializeField]
		private List<float> _levelExp;

		public float AttractionRange => _characterStats.attractionRange;

		public float MaxHealth => _characterStats.maxHealth;

		public List<float> LevelExp => _levelExp;
	}
}