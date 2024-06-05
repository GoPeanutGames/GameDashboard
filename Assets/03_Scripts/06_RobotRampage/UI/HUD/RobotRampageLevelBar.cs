using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageLevelBar: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private TMP_Text _levelText;

		[SerializeField]
		private Image _levelBar;

		private void OnEnable()
		{
			RobotRampageLevelUIEvents.OnUpdateUILevel += OnUpdateLevelText;
			RobotRampageLevelUIEvents.OnUpdateUIExp += OnUpdateLevelBar;
		}

		private void OnDisable()
		{
			RobotRampageLevelUIEvents.OnUpdateUILevel -= OnUpdateLevelText;
			RobotRampageLevelUIEvents.OnUpdateUIExp -= OnUpdateLevelBar;
		}

		private void Start()
		{
			OnUpdateLevelText(1);
			OnUpdateLevelBar(0,1);
		}

		private void OnUpdateLevelText(int level)
		{
			_levelText.text = $"L<size=24>v</size> {level}";
		}

		private void OnUpdateLevelBar(float current, float max)
		{
			_levelBar.fillAmount = current/max;
		}
	}
}