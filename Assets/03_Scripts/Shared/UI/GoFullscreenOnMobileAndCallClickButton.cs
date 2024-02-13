using PeanutDashboard.Utils.WebGL;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PeanutDashboard.Shared.UI
{
	public class GoFullscreenOnMobileAndCallClickButton: Button
	{
		public override void OnPointerDown(PointerEventData eventData)
		{
			Debug.Log($"{nameof(GoFullscreenOnMobileAndCallClickButton)}::{nameof(OnPointerDown)}");
			if (WebGLUtils.IsWebMobile){
				Debug.Log($"{nameof(GoFullscreenOnMobileAndCallClickButton)}::{nameof(OnPointerDown)} - trigger full screen");
				Screen.fullScreen = true;
			}
			base.OnPointerDown(eventData);
		}
	}
}