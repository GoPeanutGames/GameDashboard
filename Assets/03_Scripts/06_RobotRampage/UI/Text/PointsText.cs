using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class PointsText: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Text _pointsText;

		private void Awake()
		{
			_pointsText = GetComponent<TMP_Text>();
			_pointsText.text = UserService.PointsAmount.ToString();
		}

		private void OnEnable()
		{
			UserService.pointsUpdated += OnPointsUpdated;
		}

		private void OnDisable()
		{
			UserService.pointsUpdated -= OnPointsUpdated;
		}

		private void OnPointsUpdated()
		{
			Debug.Log($"{nameof(PointsText)}::{nameof(OnPointsUpdated)}");
			_pointsText.text = UserService.PointsAmount.ToString();
		}
	}
}