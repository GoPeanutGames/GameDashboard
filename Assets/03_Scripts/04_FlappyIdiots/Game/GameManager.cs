using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
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

    [System.Serializable]
    public class MetaMaskAuthData
    {
        public string address;
        public string signature;
    }

    [Serializable]
    public class ChangeUserNameData
    {
        public string signature;
        public string address;
        public string nickname;
    }


    public class GameManager : MonoBehaviour
    {
        public GameState state = GameState.Connected;
        public string gameSceneName = "Game";
        public string leaderBoardSceneName = "Leadeboard";
        public int MetamaskPoints = 9999;
        public int GameScore = 0;

        private string _lastUsername = "Disconnected";
        private MetaMaskAuthData _authData = null;
        public Text authenticationInfoText;
        private string authneticationInfo;
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
        public BackendAuthenticationManager _backendAuthenticationManager;

        public UnityEngine.UI.Text MetamaskPointsText;
        public UnityEngine.UI.InputField UsernameInputField;
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
                UsernameInputField.text = "Guest";
            }
            else
            {
                UsernameInputField.interactable = true;
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

            UsernameInputField.interactable = false;
            UsernameInputField.text = _lastUsername;
            var ok = AuthenticationEvents.Instance;
            ok.AuthenticationDataRetrievalSuccess += ((str) => { authenticationInfoText.text = "address= " + str.address + " signature= " + str.signature; });
            ok.AuthenticationDataRetrievalFail += (() => { authenticationInfoText.text = "Auth Failed"; });
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
        public void RequestChangeUsername(string newName)
        {
            if (_authData == null)
            {
                UsernameInputField.text = _lastUsername;
                return;
            }

            Debug.Log("REQUESTED CHANGED NAME" + newName);
            ChangeUserNameData formData = new()
            {
                signature = _authData.signature,
                address = _authData.address,
                nickname = newName,
            };
            ServerService.PostDataToServer<PlayerApi>(PlayerApi.ChangeNickName, JsonUtility.ToJson(formData), ((strRespo) =>
            {
                _lastUsername = newName;
                UsernameInputField.text = newName;
                OnConnected();
            }), ((strfailure) => { UsernameInputField.text = _lastUsername; }));
        }
        public void OnConnectClicked()
        {
#if UNITY_EDITOR
            _authData = new();
            _authData.signature = "0x559737c141943d2d9d1b3b3788eb897742b7a322990ed262522cff7dd953e76d3299926455bd61c3fe534e7b445796b3344e49fc7695ee881846b6cfcfb887f81b";
            _authData.address = "0x8121267d0d9261B2BeF321b42e3a0FE7E472fAb8";

            ServerService.GetDataFromServer<PlayerApi, GetGeneralDataResponse, string>(PlayerApi.GetGeneralData, ((resp) =>
            {
                if (resp.nickname == null || resp.nickname == "")
                {
                    var randomName = "user" + Random.Range(100000, 999999);
                    UsernameInputField.text = randomName;
                    ChangeUserNameData formData = new()
                    {
                        signature = _authData.signature,
                        address = _authData.address,
                        nickname = randomName
                    };
                    ServerService.PostDataToServer<PlayerApi>(PlayerApi.ChangeNickName, JsonUtility.ToJson(formData), ((strRespo) =>
                    {
                        Debug.Log("NICKNAME SUCESSFULY UPDATED:\n" + strRespo);
                        UsernameInputField.text = randomName;
                        OnConnected();

                    }), ((strfailure) => { }));
                }
                else
                {
                    UsernameInputField.text = resp.nickname;
                    OnConnected();
                }
            }), _authData.address);


            return;
#else
            var webInfoStr = LocalStorageManager.GetString("web3Info");
            if (webInfoStr != null)
            {
                var authData = JsonUtility.FromJson<MetaMaskAuthData>(webInfoStr);
                if (authData != null)
                {
                    _authData = authData;
                    ServerService.GetDataFromServer<PlayerApi, GetGeneralDataResponse, string>(PlayerApi.GetGeneralData, ((resp) =>
                    {
                        if (resp.nickname == null || resp.nickname == "")
                        {
                            var randomName = "user" + Random.Range(100000, 999999);
                            _lastUsername = randomName;
                            UsernameInputField.text = randomName;
                            ChangeUserNameData formData = new()
                            {
                                signature = _authData.signature,
                                address = _authData.address,
                                nickname = randomName
                            };
                            ServerService.PostDataToServer<PlayerApi>(PlayerApi.ChangeNickName, JsonUtility.ToJson(formData), ((strRespo) =>
                            {
                                Debug.Log("NICKNAME SUCESSFULY UPDATED:\n" + strRespo);
                                _lastUsername = randomName;
                                UsernameInputField.text = randomName;
                                OnConnected();

                            }), ((strfailure) => { }));
                        }
                        else
                        {
                            _lastUsername = resp.nickname;
                            UsernameInputField.text = resp.nickname;
                            OnConnected();
                        }
                    }), authData.address);
            
                }
            }
            
#endif
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