using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageUpgradeChoice: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private Image _background;

		[SerializeField]
		private Button _button;

		[SerializeField]
		private BaseUpgrade _currentUpgrade;

		private UnityAction<BaseUpgrade> _onUpgradeChosen;

		private void OnEnable()
		{
			_button.onClick.AddListener(OnClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnClick);
		}

		public void AddListener(UnityAction<BaseUpgrade> listener)
		{
			_onUpgradeChosen += listener;
		}
		
		public void RemoveListener(UnityAction<BaseUpgrade> listener)
		{
			_onUpgradeChosen -= listener;
		}
		
		public void SetupChoice(BaseUpgrade baseUpgrade)
		{
			_currentUpgrade = baseUpgrade;
			_background.sprite = baseUpgrade.Background;
		}

		private void OnClick()
		{
			_onUpgradeChosen.Invoke(_currentUpgrade);
		}
	}
}