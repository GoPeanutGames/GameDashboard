using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSGameController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _freeComputerLogicPrefab;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private bool _spawnedLogicController = false;
		
		private void OnEnable()
		{
			RPSUIEvents.OnPlayButtonClick += OnPlayButtonClick;
			RPSClientGameEvents.OnYouWonGame += ResetSpawn;
			RPSClientGameEvents.OnYouLostGame += ResetSpawn;
		}

		private void OnDisable()
		{
			RPSUIEvents.OnPlayButtonClick -= OnPlayButtonClick;
			RPSClientGameEvents.OnYouWonGame -= ResetSpawn;
			RPSClientGameEvents.OnYouLostGame -= ResetSpawn;
		}

		private void OnPlayButtonClick()
		{
			if (!_spawnedLogicController){
				SpawnLogicController();
			}
		}

		private void SpawnLogicController()
		{
			_spawnedLogicController = true;
			Instantiate(_freeComputerLogicPrefab);
		}

		private void ResetSpawn()
		{
			_spawnedLogicController = false;
		}
	}
}

