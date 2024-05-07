using UnityEngine;
using System.Collections;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }


        public AudioSource EffectsAudioSource;

        private AudioSource _currentPlayingMusicSource = null;

        public AudioClip VictoryAudioClip, JumpAudioClip;

        private bool isMute;
        private AudioSource[] _allSource;
        private Coroutine _lastFadeIn, _lastFadeOut;
        private void Awake()
        {
            // Ensure only one instance of MusicManager exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep the MusicManager persistent between scenes
            }
            else
            {
                Destroy(gameObject); // Destroy duplicate instances
                return;
            }
            _allSource = new AudioSource[] { TitleAudioSource, GameAudioSource, LeaderboardAudioSource };
        }

        public void SetMusicSpeed(float speedRatio)
        {
            var newPitch = 1f + (speedRatio - 1f) * 0.2f;
            
            _currentPlayingMusicSource.pitch = newPitch;
        }

        public void Mute()
        {
            if (!isMute)
            {
                isMute = true;
                _currentPlayingMusicSource.Pause();
                VictorySource.Stop();
            }
            else
            {
                isMute = false;
                _currentPlayingMusicSource.Play();
            }
        }

        private void PlayIfNotMute(AudioSource source, bool isMusic = true)
        {
            if (!isMute)
            {
                VictorySource.Stop();
                if (isMusic)
                {
                    _currentPlayingMusicSource.Stop();
                    _currentPlayingMusicSource = source;
                }
                if (isMusic)
                {
                    source.Play();
                }
                else
                {
                    source.PlayOneShot(source.clip);
                }
                if (_currentPlayingMusicSource.volume == reduceVolumeOnVictory)
                {
                    _currentPlayingMusicSource.volume = 1f;
                }
            }
        }

        public void PlayJumpSound()
        {
            if (JumpAudioClip != null)
            {
                PlaySound(JumpAudioClip);
            }
        }
        public void PlaySound(AudioClip clip)
        {
            if (EffectsAudioSource != null)
            {
                EffectsAudioSource.clip = clip;
                PlayIfNotMute(EffectsAudioSource, false);
            }
        }

        public AudioSource TitleAudioSource;
        public AudioSource GameAudioSource;
        public AudioSource LeaderboardAudioSource;
        public AudioSource VictorySource;
        public float FadeInDuration = 1f;
        public float FadeOutDuration = 0.5f;
        public float reduceVolumeOnVictory = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            // Ensure that all audio sources start with zero volume
            TitleAudioSource.volume = 0f;
            GameAudioSource.volume = 0f;
            LeaderboardAudioSource.volume = 0f;
        }

        public void PlayTrack(AudioSource source)
        {
            if (_currentPlayingMusicSource == null)
            {
                _currentPlayingMusicSource = source;
                _currentPlayingMusicSource.volume = 1;
                PlayIfNotMute(_currentPlayingMusicSource);
            }
            else
            {
                Crossfade(_currentPlayingMusicSource, source);
            }
        }

        public void PlayVictory()
        {
            var reduceVolumeDuration = 9f;
            if (!isMute)
            {
                StartCoroutine(ReduceVolume(reduceVolumeDuration));
                VictorySource.PlayOneShot(VictoryAudioClip);
            }
        }


        IEnumerator ReduceVolume(float duration)
        {
            float timer = 0f;
            float startVolume = _currentPlayingMusicSource.volume;
            if (!isMute)
            {
                foreach (var source in _allSource)
                {
                    source.volume = reduceVolumeOnVictory;
                }
            }

            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            if (!VictorySource.isPlaying)
            {
                foreach (var source in _allSource)
                {
                    StartCoroutine(FadeIn(source, FadeInDuration, false));
                }
            }
        }


        // Function to fade between two audio sources
        public void Crossfade(AudioSource from, AudioSource to)
        {
            if (_lastFadeIn != null)
            {
                StopCoroutine(_lastFadeIn);

            }
            if (_lastFadeOut != null)
            {
                StopCoroutine(_lastFadeOut);
            }
            _lastFadeOut = StartCoroutine(FadeOut(from, to));
            //   StartCoroutine(FadeIn(to));
        }

        // Coroutine to fade out an audio source
        IEnumerator FadeOut(AudioSource audioSource, AudioSource dst)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeOutDuration;
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = 0f;

            _lastFadeIn = StartCoroutine(FadeIn(dst));
        }

        IEnumerator FadeIn(AudioSource dst, float initialVolume = 0f, bool play = true)
        {
            dst.volume = initialVolume;
            if (play)
            {
                PlayIfNotMute(dst);
            }


            while (dst.volume < 1f)
            {
                dst.volume += Time.deltaTime / FadeInDuration;
                yield return null;
            }

            yield return null;
        }
    }
}