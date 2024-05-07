using System.Collections;
using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class Asteroid : MonoBehaviour
    {
        public float Speed = 1f;
        public float animationDuration = 0.5f;
        public bool HasRandomSize = false;
        public bool isMoving = false;
        public float Size = 1.0f;
        public float MovingRatio = 0.35f;
        public float SmallRatio = 0.35f;
        // Start is called before the first frame update
        void Start()
        {
            if (HasRandomSize && Random.Range(0f, 1f) > 1 - SmallRatio)
            {
                Size = Random.Range(0.55f, 1f);
                gameObject.transform.localScale = gameObject.transform.localScale * Size; ;
            }
            if (isMoving && Random.Range(0f, 1f) > 1 - MovingRatio)
            {
                var levitatingRock = GetComponent<LevitatingRock>();
                if (levitatingRock != null)
                {
                    levitatingRock.floatHeight = Random.Range(1.5f, 3f);
                    levitatingRock.floatSpeed = Random.Range(0.5f, 1.5f);
                }
            }
        }
        public bool HasExploded = false;
        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Instance.state == GameState.Playing)
            {
                var newPos = transform.position;
                newPos.x = newPos.x - Speed * 0.1f;
                transform.position = newPos;
            }
        }
        public void FadeOut(float duration)
        {
            HasExploded = true;
            StartCoroutine(FadeOutCoroutine(duration));
        }


        IEnumerator FadeOutCoroutine(float duration)
        {
            var renderer = gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {

                // Store the initial color of the sprite
                Color initialColor = renderer.color;

                // Calculate the rate of change per frame
                float fadeSpeed = 1f / duration;

                // Initialize the timer
                float timer = 0f;

                // Loop until the timer exceeds the fade duration
                while (timer < 1f)
                {
                    // Update the timer based on the elapsed time since the last frame
                    timer += Time.deltaTime * fadeSpeed;

                    // Calculate the new alpha value based on the timer
                    float newAlpha = Mathf.Lerp(initialColor.a, 0f, timer);

                    // Create a new color with the updated alpha value
                    Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);

                    // Apply the new color to the sprite renderer
                    renderer.color = newColor;

                    // Wait for the end of the frame before continuing the loop
                    yield return null;
                }
            }
            Destroy(gameObject);
            yield return null;
        }

        public void Explode()
        {
            HasExploded = true;
            StartCoroutine(ExplodeCoroutine());
        }

        IEnumerator ExplodeCoroutine()
        {
            var elapsedTime = 0f;
            var colliders = GetComponents<Collider2D>();

            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            var animator = GetComponent<Animator>();
            var renderer = GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                //renderer.enabled = false;
            }
            if (animator != null)
            {
                animator.enabled = true;
            }
            // Fade out the canvas gradually
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
            yield return null;
        }
    }
}