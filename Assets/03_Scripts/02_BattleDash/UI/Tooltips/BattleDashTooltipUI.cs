#if !SERVER
using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.WebGL;
#endif
using PeanutDashboard.Utils;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._02_BattleDash.UI
{
	public class BattleDashTooltipUI: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField] 
		private GameObject _desktopTooltips;

		[SerializeField]
		private GameObject _mobileTooltips;
		
		[SerializeField]
		private GameObject _startNowButton;
		
		[SerializeField]
		private GameObject _closeButton;
        
#if !SERVER
		private void OnEnable()
		{
			ClientUIEvents.OnShowTooltips += OnShowTooltips;
			ClientUIEvents.OnHideTooltips += OnHideTooltips;
		}

		private void OnDisable()
		{
			ClientUIEvents.OnShowTooltips -= OnShowTooltips;
			ClientUIEvents.OnHideTooltips -= OnHideTooltips;
		}

		private void OnShowTooltips(bool first)
		{
			if (WebGLUtils.IsWebMobile){
				_mobileTooltips.Activate();
			}
			else{
				_desktopTooltips.Activate();
			}
			if (first){
				_startNowButton.Activate();
			}
			else{
				_closeButton.Activate();
			}
		}

		private void OnHideTooltips()
		{
			_mobileTooltips.Deactivate();
			_desktopTooltips.Deactivate();
			_startNowButton.Deactivate();
			_closeButton.Deactivate();
		}
#endif
	}
}