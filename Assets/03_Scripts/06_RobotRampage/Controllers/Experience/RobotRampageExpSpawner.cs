using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageExpSpawner: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageExpCollection _expCollection;
		
		[SerializeField]
		private GameObject _expPrefab;

		private void OnEnable()
		{
			RobotRampageExpSpawnEvents.OnSpawnExpType += OnSpawnExpType;
		}

		private void OnDisable()
		{
			RobotRampageExpSpawnEvents.OnSpawnExpType -= OnSpawnExpType;
		}

		private void OnSpawnExpType(RobotRampageExpType expType, Vector3 position)
		{
			RobotRampageExpData data = _expCollection.GetExperienceData(expType);
			GameObject expDrop = Instantiate(_expPrefab, position, Quaternion.identity);
			expDrop.GetComponent<RobotRampageExpController>().Setup(data.ExpImage, data.ExpAmount);
		}
	}
}