using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class LevitatingRock : MonoBehaviour
    {
        public float floatSpeed = 1f; // Adjust this to change the speed of the float
        public float floatHeight = 0.5f; // Adjust this to change the height of the float
        private Vector3 startPos;
        private UnityEngine.UI.Image image;

        float randomFloat = 0f;
        void Start()
        {
            randomFloat = Random.Range(0f, 1f);
            image = GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                startPos = image.rectTransform.anchoredPosition;
            }
            else
            {
                startPos = transform.position;
            }
        }

        void Update()
        {
            // Calculate the new position using a sine wave to create the floating effect
            float newY = startPos.y + Mathf.Sin((randomFloat * Mathf.PI) + Time.time * floatSpeed) * floatHeight;

            // Update the position of the GameObject
            if (image != null)
            {
                Vector3 newPos = new Vector3(startPos.x, newY, startPos.z);
                image.rectTransform.anchoredPosition = newPos;
            }
            else
            {
                Vector3 newPos = new Vector3(transform.position.x, newY, transform.position.z);
                transform.position = newPos;
            }
        }
    }
}