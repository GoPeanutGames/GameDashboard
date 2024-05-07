using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class MuteButton : MonoBehaviour
    {
        public Sprite UnmuteSprite;
        public Sprite MuteSprite;
        public bool IsMute = false;
        private UnityEngine.UI.Image ImageRenderer;
        // Start is called before the first frame update
        void Start()
        {
            ImageRenderer = GetComponent<UnityEngine.UI.Image>();
        }

        public void Mute()
        {
            if (IsMute)
            {
                ImageRenderer.sprite = UnmuteSprite;
                IsMute = false;
            }
            else
            {
                ImageRenderer.sprite = MuteSprite;
                IsMute = true;
            }
            SoundManager.Instance.Mute();
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}