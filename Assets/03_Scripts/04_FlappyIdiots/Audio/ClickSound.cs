using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PeanutDashboard._04_FlappyIdiots
{

    public class ClickSound : MonoBehaviour
    {
        public AudioClip clickAudio;
        public float disableSoundDuration = 0.25f; // Duration to disable the button in seconds
        private bool canPlaySound = true;
        void Start()
        {

            var button = GetComponent<Button>();
            if (button != null && clickAudio != null)
            {
                canPlaySound = true;
                button.onClick.AddListener(() =>
                {
                    StartCoroutine(PlaySoundAndDisableButton());
                });
            }
        }

        IEnumerator PlaySoundAndDisableButton()
        {
            // Play the click sound
            if (canPlaySound)
            {
                SoundManager.Instance.PlaySound(clickAudio);

                // Disable the button
                var button = GetComponent<Button>();
                if (button != null)
                {
                    canPlaySound = false;

                    // Wait for the specified duration
                    yield return new WaitForSeconds(disableSoundDuration);
                    canPlaySound = true;
                }
            }
        }
    }
}