#if !UNITY_EDITOR
using PeanutDashboard.Utils.WebGL;
using UnityEngine;
#endif
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PeanutDashboard.Shared.UI
{
	public class GoFullscreenOnMobileAndCallClickButton: Button
	{
		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
#if !UNITY_EDITOR
			if (WebGLUtils.IsWebMobile){
				Debug.Log($"{nameof(GoFullscreenOnMobileAndCallClickButton)}::{nameof(OnPointerDown)} - go full screen");
				Screen.fullScreen = true;
			}
#endif
		}
	}
}