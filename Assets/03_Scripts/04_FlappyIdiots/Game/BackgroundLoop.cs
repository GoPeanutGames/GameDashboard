using System;
using System.Collections;
using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class BackgroundLoop : MonoBehaviour
    {
        public static BackgroundLoop instance; // Singleton instance

        public float scrollSpeed = 1.0f;
        public Transform[] backgrounds;
        private float backgroundWidth;
        private Vector3[] originalPositions;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            // Calculate the width of one background image
            backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

            // Store the original positions of the backgrounds
            originalPositions = new Vector3[backgrounds.Length];
            for (int i = 0; i < backgrounds.Length; i++)
            {
                originalPositions[i] = backgrounds[i].position;
            }
        }

        private void FixedUpdate()
        {
            var scale = this.gameObject.transform.localScale.x;
            // Move backgrounds based on scroll speed direction
            float moveDirection = scrollSpeed > 0 ? -1f : 1f;
            float moveDistance = Mathf.Abs(scrollSpeed) * Time.deltaTime;
            float stepDistance = moveDirection * moveDistance;
            var firstBackground = backgrounds[0];
            var secondBackground = backgrounds[1];
            if (moveDirection == 1)
            {
                firstBackground = backgrounds[1];
                secondBackground = backgrounds[0];
            }
            var halfWidth = (backgroundWidth / 2) * scale;
            Vector3 newPos = firstBackground.position;
            newPos.x += stepDistance;
            firstBackground.position = newPos;
            var newPosSecond = firstBackground.position;
            newPosSecond.x += halfWidth * -moveDirection;
            secondBackground.position = newPosSecond;
            Vector3 resetPos = secondBackground.position;
            resetPos.x += resetPos.x + halfWidth * -moveDirection;
            if (moveDirection == -1 && firstBackground.position.x < -halfWidth)
            {
                firstBackground.position = resetPos;
                backgrounds = new[] { secondBackground, firstBackground };
            }
            else if (moveDirection == 1 && firstBackground.position.x > halfWidth)
            {
                firstBackground.position = resetPos;
                backgrounds = new[] { firstBackground, secondBackground };
            }
        }

        public void ResetBackgroundPositions(float duration)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                StartCoroutine(LerpBackgroundPosition(backgrounds[i], originalPositions[i], duration));
            }
        }

        public void ChangeScrollSpeed(float newSpeed, float duration, Action endAction = null)
        {
            StartCoroutine(LerpScrollSpeed(newSpeed, duration, endAction));
        }

        private IEnumerator LerpBackgroundPosition(Transform background, Vector3 targetPosition, float duration)
        {
            float timeElapsed = 0f;
            Vector3 startPosition = background.position;

            while (timeElapsed < duration)
            {
                background.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            background.position = targetPosition; // Ensure final position is accurate
        }

        private IEnumerator LerpScrollSpeed(float newSpeed, float duration, Action endAction)
        {
            float timeElapsed = 0f;
            float originalSpeed = scrollSpeed;

            while (timeElapsed < duration)
            {
                scrollSpeed = Mathf.Lerp(originalSpeed, newSpeed, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            scrollSpeed = newSpeed; // Ensure final speed is accurate
            if (endAction != null)
            {
                endAction();
            }
        }
    }
}