using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageRetryButton: MonoBehaviour
	{
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;
        
		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnRetryButtonClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnRetryButtonClick);
		}

		private void OnRetryButtonClick()
		{
			SceneManager.LoadScene(1);
		}
	}
}