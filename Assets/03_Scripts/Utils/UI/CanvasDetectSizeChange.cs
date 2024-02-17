using System.Collections.Generic;
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
		public int minSize;
		public List<GameObject> containers;

		private RectTransform _rectTransform;
		protected override void Awake() => _rectTransform = (RectTransform)transform;

		protected override void Start()
		{
			OnRectTransformDimensionsChange();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (_rectTransform != null){
				Debug.Log($"{nameof(CanvasDetectSizeChange)}::{nameof(OnRectTransformDimensionsChange)} - Detected change, rebuilding layout");
				Debug.Log($"{nameof(CanvasDetectSizeChange)}::{nameof(OnRectTransformDimensionsChange)} - Rect size: {_rectTransform.rect.size}");
				if (_rectTransform.rect.size.x < minSize){
					this.GetComponent<Canvas>().sortingOrder = activeOnMobile ? sortOrder : 0;
					foreach (GameObject container in containers){
						container.SetActive(activeOnMobile);
					}
				}
				else if (_rectTransform.rect.size.x >= minSize){
					this.GetComponent<Canvas>().sortingOrder = !activeOnMobile ? sortOrder : 0;
					foreach (GameObject container in containers){
						container.SetActive(!activeOnMobile);
					}
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
				Canvas.ForceUpdateCanvases();
			}
		}
	}
}