using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class GemsText: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Text _gemsText;

		private void Awake()
		{
			_gemsText = GetComponent<TMP_Text>();
			_gemsText.text = UserService.GemsAmount.ToString();
		}

		private void OnEnable()
		{
			UserService.gemsUpdated += OnGemsUpdated;
		}

		private void OnDisable()
		{
			UserService.gemsUpdated -= OnGemsUpdated;
		}

		private void OnGemsUpdated()
		{
			Debug.Log($"{nameof(GemsText)}::{nameof(OnGemsUpdated)}");
			_gemsText.text = UserService.PointsAmount.ToString();
		}
	}
}