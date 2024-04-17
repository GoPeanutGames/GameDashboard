using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PeanutDashboard._04_FlappyIdiots
{
    public enum GameState
    {
        Disconnected,
        Connected,
        Playing,
        GameOver,
        Leaderboard
    }
    public class GameManager : MonoBehaviour
    {
        public GameState state = GameState.Connected;
        public string gameSceneName = "Game";
        public string leaderBoardSceneName = "Leadeboard";
        public int MetamaskPoints = 9999;
        public string UserName = "Flappy";
        public int GameScore = 0;

        private bool connected = false;

        [SerializeField]
        private BackgroundLoop _backgroundLoop;

        public float transitionDuration = 0.5f;
        public Scrollbar LeaderboardScrollbar;
        public float TitleBackgroundSpeed = 1.0f;
        public float playingBackgroundSpeed = 2.0f;
        public SpaceshipController SpaceShip;
        public float spaceshipAppearDuration = 1.0f;
        public static GameManager Instance { get; private set; }
        private GameObject SpawnObject;
        public GameObject[] SpawnObjects;

        public float SpawnTimeMin = 3f;
        public float SpawnTimeMax = 3f;
        public float SpawnHeightRange = 2.5f;
        public CanvasGroup StartUICanvas;
        public CanvasGroup StartButtonCanvasGroup;
        public CanvasGroup ConnectButtonCanvasGroup;
        public CanvasGroup UsernameOKButtonCanvasGroup;
        public CanvasGroup UsernameCanvasGroup;
        public CanvasGroup UsernameLobbyCanvasGroup;
        public CanvasGroup MetamaskCanvasGroup;
        public CanvasGroup InGameSettingsCanvasGroup;
        public CanvasGroup LeaderboardUICanvasGroup;
        public CanvasGroup GameOverCanvasGroup;


        public UnityEngine.UI.Text MetamaskPointsText;
        public UnityEngine.UI.Text UsernameText;
        public UnityEngine.UI.Text ScoreText;
        public UnityEngine.UI.Text GameOverScoreText;
        public UnityEngine.UI.Text GameOverPointsEarnedText;

        public TopPlayerLine[] leaderBoardPlayers;

        private bool isGuest = false;

        public void UpdateLeaderBoard()
        {
            foreach (var topPlayer in leaderBoardPlayers)
            {
                topPlayer.SetData("", "", "");
            }
        }
        public void UpdateScoreText(bool updateGameOver = true)
        {
            if (updateGameOver)
            {
                if (GameOverScoreText != null)
                {
                    GameOverScoreText.text = GameScore.ToString();
                }

                if (GameOverPointsEarnedText != null)
                {
                    GameOverPointsEarnedText.text = "And you've earned <color=#66B9F1>" + GameScore * 300 + "</color> points";
                }
            }
            if (ScoreText != null)
            {
                ScoreText.text = ScoreText.ToString();
            }
        }
        void Start()
        {
            UpdateLeaderBoard();
            state = GameState.Disconnected;
            SoundManager.Instance.PlayTrack(SoundManager.Instance.TitleAudioSource);

            MetamaskCanvasGroup?.gameObject.SetActive(false);
            StartButtonCanvasGroup?.gameObject.SetActive(false);
            InGameSettingsCanvasGroup?.gameObject.SetActive(false);
            UsernameOKButtonCanvasGroup?.gameObject.SetActive(false);
            GameOverCanvasGroup?.gameObject.SetActive(false);
            StartUICanvas?.gameObject.SetActive(true);
            UsernameCanvasGroup?.gameObject.SetActive(true);
            ConnectButtonCanvasGroup?.gameObject.SetActive(true);
            UsernameLobbyCanvasGroup?.gameObject.SetActive(true);
            StartButtonCanvasGroup.gameObject.SetActive(false);
            LeaderboardUICanvasGroup?.gameObject.SetActive(false);

            SpawnObject = SpawnObjects[Random.Range(0, SpawnObjects.Length)];
            UpdateScoreText();
            Spawn();
        }
        public void OnPlayAgain()
        {
            StartCoroutine(PlayAgainTransition());
        }
        public void OnConnected()
        {
            if (isGuest)
            {
                UserName = "Guest";
            }
            connected = true;
            StartCoroutine(ConnectedTransition());
        }

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
        }

        public void OnGameOver()
        {
            StartCoroutine(GameOverTransition());
        }

        IEnumerator GameOverTransition()
        {
            SoundManager.Instance.PlayVictory();
            UpdateScoreText();
            state = GameState.GameOver;
            GameOverCanvasGroup.interactable = false;
            float fadeOutDuration = 0.5f;

            StartCoroutine(CanvasTransition(new CanvasGroup[] { GameOverCanvasGroup }, fadeOutDuration, false));
            GameOverCanvasGroup.interactable = true;

            yield return null;
        }

        public void OnHomePressed()
        {
            StartCoroutine(HomeTransition());
        }

        IEnumerator HomeTransition()
        {
            GameScore = 0;
            ConnectButtonCanvasGroup.interactable = false;
            GameOverCanvasGroup.interactable = false;
            SpaceShip.gameObject.SetActive(false);
            var asteroids = GameObject.FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
            SoundManager.Instance.PlayTrack(SoundManager.Instance.TitleAudioSource);
            UsernameCanvasGroup.gameObject.SetActive(true);
            UsernameLobbyCanvasGroup.gameObject.SetActive(true);
            UsernameLobbyCanvasGroup.alpha = 1.0f;
            float fadeOutDuration = 0.5f;
            foreach (var asteroid in asteroids)
            {
                asteroid.FadeOut(fadeOutDuration);
            }

            CanvasGroup[] removedCanvases = new CanvasGroup[] { InGameSettingsCanvasGroup, GameOverCanvasGroup, LeaderboardUICanvasGroup };
            if (_backgroundLoop != null)
            {
                _backgroundLoop.ChangeScrollSpeed(200, fadeOutDuration, () =>
                {
                    _backgroundLoop.ChangeScrollSpeed(TitleBackgroundSpeed, fadeOutDuration);
                });
            }
            if (connected)
            {
                StartCoroutine(CanvasTransition(removedCanvases, fadeOutDuration, true, () =>
                {

                    StartCoroutine(CanvasTransition(new CanvasGroup[] { UsernameOKButtonCanvasGroup, StartButtonCanvasGroup, StartUICanvas }, fadeOutDuration, false));
                }));

                state = GameState.Connected;
            }
            else
            {
                StartCoroutine(CanvasTransition(removedCanvases, fadeOutDuration, true, () =>
                {

                    StartCoroutine(CanvasTransition(new CanvasGroup[] { ConnectButtonCanvasGroup, StartUICanvas }, fadeOutDuration, false));
                }));
                ConnectButtonCanvasGroup.interactable = true;
                state = GameState.Disconnected;
            }
            yield return null;
        }

        IEnumerator PlayAgainTransition()
        {
            GameScore = 0;
            GameOverCanvasGroup.interactable = false;
            SpaceShip.gameObject.SetActive(true);
            SpaceShip.Appear();
            var asteroids = GameObject.FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
            float fadeOutDuration = 0.5f;
            foreach (var asteroid in asteroids)
            {
                asteroid.FadeOut(fadeOutDuration);
            }

            CanvasGroup[] removedCanvases = new CanvasGroup[] { GameOverCanvasGroup };
            if (_backgroundLoop != null)
            {
                _backgroundLoop.ChangeScrollSpeed(100, fadeOutDuration, () =>
                {
                    _backgroundLoop.ChangeScrollSpeed(playingBackgroundSpeed, fadeOutDuration);
                });
            }

            StartCoroutine(CanvasTransition(removedCanvases, fadeOutDuration, true, () =>
            {
                UpdateScoreText();
            }));

            state = GameState.Playing;

            yield return null;
        }

        private void UpdateScoreAndUser()
        {
            if (state == GameState.Disconnected)
            {
                return;
            }

            if (UsernameText != null)
            {
                UsernameText.text = UserName;
            }
            if (ScoreText != null)
            {
                ScoreText.text = GameScore.ToString();
            }
            if (MetamaskPointsText != null)
            {
                MetamaskPointsText.text = MetamaskPoints.ToString();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            UpdateScoreAndUser();
        }

        public void OnStartClicked()
        {
            StartCoroutine(StartGameTransition());
        }
        public void OnLeaderboardClicked()
        {
            StartCoroutine(LeaderboardTransition());
        }
        IEnumerator ConnectedTransition()
        {
            ConnectButtonCanvasGroup.interactable = false;
            StartCoroutine(CanvasTransition(ConnectButtonCanvasGroup, 0.5f, true, () =>
            {

                StartCoroutine(CanvasTransition(new CanvasGroup[] { UsernameOKButtonCanvasGroup, MetamaskCanvasGroup, StartButtonCanvasGroup }, 0.5f, false));
            }));

            state = GameState.Connected;
            yield return null;
        }

        IEnumerator StartGameTransition()
        {
            StartCoroutine(CanvasTransition(new CanvasGroup[] { StartUICanvas, UsernameOKButtonCanvasGroup, UsernameLobbyCanvasGroup }, transitionDuration));
            StartCoroutine(CanvasTransition(InGameSettingsCanvasGroup, transitionDuration, false));
            if (_backgroundLoop != null)
            {
                _backgroundLoop.ChangeScrollSpeed(100, transitionDuration, () =>
                {
                    _backgroundLoop.ChangeScrollSpeed(playingBackgroundSpeed, spaceshipAppearDuration);
                });
            }
            state = GameState.Playing;
            SoundManager.Instance.PlayTrack(SoundManager.Instance.GameAudioSource);
            SpaceShip.gameObject.SetActive(true);
            SpaceShip.Appear(spaceshipAppearDuration);
            yield return null;
        }

        IEnumerator LeaderboardTransition()
        {
            if (_backgroundLoop != null)
            {
                _backgroundLoop.ChangeScrollSpeed(-100, 1f, () => { _backgroundLoop.ChangeScrollSpeed(-1f, 0.5f); });
            }
            state = GameState.Leaderboard;
            SoundManager.Instance.PlayTrack(SoundManager.Instance.LeaderboardAudioSource);
            UsernameCanvasGroup.gameObject.SetActive(false);
            UsernameOKButtonCanvasGroup.gameObject.SetActive(false);
            StartCoroutine(CanvasTransition(new CanvasGroup[] { StartUICanvas }, transitionDuration, true, () => { StartCoroutine(CanvasTransition(LeaderboardUICanvasGroup, transitionDuration, false)); LeaderboardScrollbar.value = 1; }));
            LeaderboardScrollbar.value = 1;
            yield return null;
        }

        IEnumerator CanvasTransition(CanvasGroup canvas, float duration, bool fadeOut = true, Action onEnd = null)
        {
            yield return CanvasTransition(new CanvasGroup[] { canvas }, duration, fadeOut, onEnd);
        }
        IEnumerator CanvasTransition(CanvasGroup[] canvases, float duration, bool fadeOut = true, Action onEnd = null)
        {
            float elapsedTime = 0f;
            float startAlpha = 1f;
            float targetAlpha = 0f;
            if (!fadeOut)
            {
                targetAlpha = 1f;
                startAlpha = 0f;
                foreach (var canvas in canvases)
                    canvas.gameObject.SetActive(true);
            }
            // Fade out the canvas gradually
            while (elapsedTime < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

                foreach (var canvas in canvases)
                    canvas.alpha = newAlpha;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            foreach (var canvas in canvases)
                canvas.alpha = targetAlpha;
            if (fadeOut)
            {
                foreach (var canvas in canvases)
                    canvas.gameObject.SetActive(false);
            }
            if (onEnd != null)
            {
                onEnd();
            }
        }

        public void OnConnectClicked()
        {
            OnConnected();
        }

        public void OnPlayAsGuestClicked()
        {
            isGuest = true;
            OnConnected();
        }

        void Spawn()
        {
            if (state == GameState.Playing)
            {
                //random y position
                float y = Random.Range(-SpawnHeightRange, SpawnHeightRange);
                GameObject asteroid = Instantiate(SpawnObject, this.transform.position + new Vector3(0, y, 0), Quaternion.identity) as GameObject;
            }
            Invoke("Spawn", Random.Range(SpawnTimeMin, SpawnTimeMax));
        }


    }
}