using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI.Loading
{
	public class BattleDashLoadingUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private GameObject _loadingWrapper;
		
		[SerializeField]
		private TMP_Text _loadingText;
		
		private void OnEnable()
		{
			BattleDashLoadingEvents.OnUpdateLoadingText += OnUpdateLoadingText;
			BattleDashLoadingEvents.OnCloseLoading += OnCloseLoading;
			OnUpdateLoadingText("Loading");
		}

		private void OnDisable()
		{
			BattleDashLoadingEvents.OnUpdateLoadingText -= OnUpdateLoadingText;
			BattleDashLoadingEvents.OnCloseLoading -= OnCloseLoading;
		}

		private void OnUpdateLoadingText(string text)
		{
			_loadingText.text = $"<color=#2FD0BB>{text}</color><color=#F9D85A>...</color>";
		}

		private void OnCloseLoading()
		{
			_loadingWrapper.Deactivate();
		}
	}
}