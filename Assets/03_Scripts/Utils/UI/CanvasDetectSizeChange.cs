using PeanutDashboard.Shared.Logging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PeanutDashboard.Utils.UI
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Canvas))]
	public class CanvasDetectSizeChange : UIBehaviour
	{
		public bool activeOnMobile;
		public int sortOrder = 100;

		private RectTransform _rectTransform;
		protected override void Awake() => _rectTransform = (RectTransform)transform;

		protected override void Start()
		{
			OnRectTransformDimensionsChange();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (_rectTransform != null){
				LoggerService.LogInfo($"{nameof(CanvasDetectSizeChange)}::{nameof(OnRectTransformDimensionsChange)} - Detected change, rebuilding layout");
				LoggerService.LogInfo($"{nameof(CanvasDetectSizeChange)}::{nameof(OnRectTransformDimensionsChange)} - Rect size: {_rectTransform.rect.size}");
				if (_rectTransform.rect.size.x < 1080){
					this.GetComponent<Canvas>().sortingOrder = activeOnMobile ? sortOrder : 0;
				}
				else if (_rectTransform.rect.size.x >= 1080){
					this.GetComponent<Canvas>().sortingOrder = !activeOnMobile ? sortOrder : 0;
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
				Canvas.ForceUpdateCanvases();
			}
		}
	}
}