using System;
using PeanutDashboard.Init;
using PeanutDashboard.Shared;
using PeanutDashboard.Shared.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._03_RockPaperScissors.Controllers
{
	public class RPSLoader: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private SceneInfo _sceneInfo;
		
		private void OnEnable()
		{
			AddressablesEvents.Instance.AddressablesInitialised += OnAddressablesInitialised;
		}

		private void OnDisable()
		{
			AddressablesEvents.Instance.AddressablesInitialised -= OnAddressablesInitialised;
		}

		private void Start()
		{
			if (AddressablesService.Instance.AreAddressablesInitialised()){
				OnAddressablesInitialised();
			}
			else{
				AddressablesService.Instance.InitialiseAddressables();
			}
		}

		private void OnAddressablesInitialised()
		{
			SceneLoaderService.Instance.LoadAndOpenScene(_sceneInfo);
		}
	}
}