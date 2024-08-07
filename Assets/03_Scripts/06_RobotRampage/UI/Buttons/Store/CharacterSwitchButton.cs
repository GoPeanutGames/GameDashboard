using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
	public class CharacterSwitchButton: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private RobotRampageCharacterData _chosenCharacter;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private Button _button;
        
		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(SwitchCharacter);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(SwitchCharacter);
		}

		private void SwitchCharacter()
		{
			RobotRampageCharacterStatsService.SetCharacter(_chosenCharacter);
		}
	}
}
