using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class NutzText: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private TMP_Text _nutzText;

		private void Awake()
		{
			_nutzText = GetComponent<TMP_Text>();
			_nutzText.text = UserService.NutzAmount.ToString();
		}

		private void OnEnable()
		{
			UserService.nutzUpdated += OnGemsUpdated;
		}

		private void OnDisable()
		{
			UserService.nutzUpdated -= OnGemsUpdated;
		}

		private void OnGemsUpdated()
		{
			Debug.Log($"{nameof(NutzText)}::{nameof(OnGemsUpdated)}");
			_nutzText.text = UserService.NutzAmount.ToString();
		}
	}
}