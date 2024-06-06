using PeanutDashboard.Utils.Misc;
using TMPro;
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
		private Image _icon;

		[SerializeField]
		private Image _levelIcon;

		[SerializeField]
		private TMP_Text _title;

		[SerializeField]
		private TMP_Text _description;

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
			_levelIcon.sprite = baseUpgrade.LevelIcon;
			_icon.sprite = baseUpgrade.Icon;
			_title.text = baseUpgrade.Title;
			_description.text = baseUpgrade.Description;
		}

		private void OnClick()
		{
			_onUpgradeChosen.Invoke(_currentUpgrade);
		}
	}
}