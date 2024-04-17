using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float hoverScale = 1.1f; // Adjust this to change the scale when hovered over
        public float transitionDuration = 0.2f; // Adjust this to change the duration of the transition
        private Vector3 originalScale;
        private bool isHovered = false;
        private Coroutine scaleCoroutine;

        void Start()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isHovered)
            {
                scaleCoroutine = StartCoroutine(ScaleButton(originalScale * hoverScale, transitionDuration));
                isHovered = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isHovered)
            {
                scaleCoroutine = StartCoroutine(ScaleButton(originalScale, transitionDuration));
                isHovered = false;
            }
        }

        private IEnumerator ScaleButton(Vector3 targetScale, float duration)
        {
            float time = 0f;
            Vector3 startScale = transform.localScale;

            while (time < duration)
            {
                transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.localScale = targetScale;
        }
    }
}