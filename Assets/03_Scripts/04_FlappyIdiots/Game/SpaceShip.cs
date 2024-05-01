using UnityEngine;
using System.Collections;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class SpaceshipController : MonoBehaviour
    {
        public float startSpeed = 5f; // The initial speed of the spaceship

        private bool canControl = false; // Whether the player can control the spaceship
        private Rigidbody2D rb; // Reference to the Rigidbody component

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private Vector3 shipRotation = Vector3.zero;
        public float RotateDownSpeed = 1;
        public float RotateUpSpeed = 1;
        
        public float OutOfScreenLimit = 3f;
        private float outOfScreenTimer = 0f;
        bool isOutScreen = false;

        public float MaxUpperAngle = 0f;
        public float MaxLowerAngle = 45f;
        private float _speedMutliplier = 1.0f;
        private float GravityScale = -2.0f;
        private float _mass = 9.0f;
        public float SpeedMultiplier
        {
            get { return _speedMutliplier; }
            set { _speedMutliplier = value;
                var rb = GetComponent<Rigidbody2D>();
                if (rb != null )
                {
                    rb.gravityScale = GravityScale * value;
                    rb.mass = _mass * value;
                }
            }
        }

        bool isDead = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ceiling"))
            {
                isOutScreen = true;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ceiling"))
            {
                isOutScreen = false;
                outOfScreenTimer = 0f;
            }
            if (other.gameObject.CompareTag("Asteroid") )
            {
                Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
                if (asteroid != null && !asteroid.HasExploded && other.transform.position.x < (transform.position.x - 2.316f) * asteroid.Size)
                {
                    OnDeath();
                }
            }
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }
        void Start()
        {
        }

        void OnDeath()
        {
            if (canControl)
            {
                GameManager.Instance.OnGameOver();
                canControl = false;
                isDead = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
                if (asteroid != null && !asteroid.HasExploded)
                {
                    asteroid.Explode();
                    Jump((_mass + jumpForce) * 0.8f, false);
                    GameManager.Instance.GameScore++;
                }
            }
        }
        void Update()
        {
            // Enable control once the spaceship has appeared
            if (canControl)
            {
                if (transform.position.x != _initialPosition.x)
                {
                    var newPos = _initialPosition;
                    newPos.y = transform.position.y;
                    transform.position = newPos;
                }
                // Check for mouse click
                if (Input.GetMouseButtonDown(0))  // 0 represents left mouse button
                {
                    // Make the spaceship jump
                    Jump((_mass * SpeedMultiplier) + jumpForce);
                }

                if (isOutScreen)
                {
                    outOfScreenTimer += Time.deltaTime;
                    if (outOfScreenTimer >= OutOfScreenLimit)
                    {
                        OnDeath();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (canControl || isDead)
            {
                AdjustRotation();
            }
        }
        public float jumpForce = 10f;

        void Jump(float value, bool fromUser = true)
        {
            if (fromUser)
            {
                SoundManager.Instance.PlayJumpSound();
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -value);
        }

        public void Appear(float duration = 2f)
        {
            // Start the appearing animation
            StartCoroutine(AppearAnimation(duration));
        }
        private void AdjustRotation()
        {
            float degreesToAdd = 0;
            if (GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                degreesToAdd = 3 * RotateUpSpeed;
            }
            else
            {
                degreesToAdd = -6 * RotateDownSpeed;
            }

            shipRotation = new Vector3(0, 0, Mathf.Clamp(shipRotation.z + degreesToAdd, MaxUpperAngle, MaxLowerAngle));
            transform.eulerAngles = shipRotation;
        }

        IEnumerator AppearAnimation(float duration)
        {
            isDead = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;

            float t = 0f;
            Vector2 startPos = new Vector2(-12f, 0f); // Start position of the spaceship (off-screen)
            Vector2 endPos = _initialPosition; // Final position of the spaceship
            transform.position = startPos;
            transform.rotation = _initialRotation;
            while (t < duration)
            {
                float progress = t / duration;
                float easedProgress = EaseInOutCubic(progress);

                // Interpolate position from start to end smoothly over transitionTime using easing function
                transform.position = Vector2.Lerp(startPos, endPos, easedProgress);
                t += Time.deltaTime;
                yield return null;
            }

            // Ensure spaceship is at the final position
            transform.position = endPos;
            // Enable control and Rigidbody
            canControl = true;
            rb.simulated = true;
        }

        float EaseInOutCubic(float x)
        {
            return x < 0.5f ? 4f * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
        }
    }
}