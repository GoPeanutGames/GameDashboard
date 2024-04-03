using PeanutDashboard._03_RockPaperScissors.Events;
using PeanutDashboard._03_RockPaperScissors.Model;
using PeanutDashboard._03_RockPaperScissors.State;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSGameController: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _freeComputerLogicPrefab;
		
		[SerializeField]
		private GameObject _freePvpLogicPrefab;
		
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
			if (RPSCurrentClientState.rpsModeType == RPSModeType.Free){
				if (RPSCurrentClientState.rpsOpponentType == RPSOpponentType.PC){
					Instantiate(_freeComputerLogicPrefab);
				}else if (RPSCurrentClientState.rpsOpponentType == RPSOpponentType.Player){
					Instantiate(_freePvpLogicPrefab);
				}
			}
		}

		private void ResetSpawn()
		{
			_spawnedLogicController = false;
		}
	}
}

