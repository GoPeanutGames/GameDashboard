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
		private GameObject _characterVisualsPrefab;
		
		[SerializeField]
		private RobotRampageCharacterStats _characterStats;

		[SerializeField]
		private List<BaseUpgrade> _startingWeapons;

		[SerializeField]
		private List<float> _levelExp;

		public GameObject CharacterVisualsPrefab => _characterVisualsPrefab;
		
		public float AttractionRange => _characterStats.attractionRange;

		public float MaxHealth => _characterStats.maxHealth;

		public float PlasmaStrength => _characterStats.plasmaStrength;

		public float Power => _characterStats.power;

		public float Speed => _characterStats.speed;

		public List<float> LevelExp => _levelExp;

		public List<BaseUpgrade> StartingWeapons => _startingWeapons;
	}
}