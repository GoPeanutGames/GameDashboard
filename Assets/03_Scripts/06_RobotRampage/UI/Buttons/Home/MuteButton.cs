using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class MuteButton: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private Sprite _muteSprite;

        [SerializeField]
        private Sprite _unMuteSprite;

        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private Image _audioImage;

        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _audioImage = GetComponent<Image>();
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnAudioButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnAudioButtonClick);
        }

        private void Start()
        {
            _audioImage.sprite = !AudioService.sfxMuted ? _muteSprite : _unMuteSprite;
        }

        private void OnAudioButtonClick()
        {
            AudioService.sfxMuted = AudioService.bgMusicMuted = !AudioService.sfxMuted;
            _audioImage.sprite = !AudioService.sfxMuted ? _muteSprite : _unMuteSprite;
        }
    }
}