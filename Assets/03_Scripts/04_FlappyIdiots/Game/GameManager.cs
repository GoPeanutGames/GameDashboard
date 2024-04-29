using Dynamitey.DynamicObjects;
using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Events;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using ZXing.QrCode.Internal;
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

    [Serializable]
    public class SessionEndResponseData
    {
        public int bubbles;
        public string sessionId;
    }

    [Serializable]
    public class SessionStartData
    {
        public string signature;
        public string address;
        public string mode;
        public string timezone;
        public string startTime;
        public string gameName;
    }

    [Serializable]
    public class SessionUpdateData
    {
        public string signature;
        public string address;
        public string sessionId;
        public int score;
    }

    [Serializable]
    public class SessionEndData
    {
        public string signature;
        public string address;
        public string sessionId;
        public int score;
        public string status;
        public string endTime;
    }
    [Serializable]
    public class SessionData
    {
        public string sessionId;
    }

    [Serializable]
    public class GameFormData
    {
        public string gameName;
    }
    [Serializable]
    public class LeaderBoardEltData
    {
        public int score;
        public string nickname;
    }
    [Serializable]
    public class LeaderBoardResponseData
    {
        public LeaderBoardEltData[] highScores;
    }
    [Serializable]
    public class WalletData
    {
        public int gems;
        public int energy;
        public int bubbles;
    }

    public class GameManager : MonoBehaviour
    {
        private string gameName = "FLAPPY_IDIOTS";

        public GameState state = GameState.Connected;
        public string gameSceneName = "Game";
        public string leaderBoardSceneName = "Leadeboard";
        private int _gameScore = 0;
        public int GameScore { set {
                _gameScore = value;
                if (value > 0)
                {
                    updateSession();
                }
                SynchronizeWallet();
            } 
            get { return _gameScore; }
        }

        private string _lastUsername = "Disconnected";
        private MetaMaskAuthData _authData = null;
        private WalletData _walletData = null;
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

        public LeaderboardContent leaderboardContent;
        private string currentSessionId = "";
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
        private int _bubbleEarned = 0;

        private bool isGuest = false;
        private string timeZone = "GMT+" + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).ToString("hh\\:mm");

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
                    GameOverPointsEarnedText.text = "And you've earned <color=#66B9F1>" + _bubbleEarned + "</color> Bubbles";
                }
            }
            if (ScoreText != null)
            {
                ScoreText.text = ScoreText.ToString();
            }
        }
        void Start()
        {
            UpdateLeaderBoardData();
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
            getTime();
        }
        public void OnPlayAgain()
        {
            StartCoroutine(PlayAgainTransition());
        }

        public string getTime()
        {
            return DateTime.Now.ToString("ddd MMM dd yyyy HH:mm:ss");
        }

        public void OnConnected()
        {
            if (isGuest)
            {
                UsernameInputField.text = "Guest";
                MetamaskCanvasGroup.alpha = 0;
            }
            else
            {
                UsernameInputField.interactable = true;

                MetamaskCanvasGroup.alpha = 1;
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
        }

        public void OnGameOver()
        {
            StartCoroutine(GameOverTransition());
        }

        IEnumerator GameOverTransition()
        {
            endSession(true);
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
            endSession(false);
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
            startNewSession();
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
            if (MetamaskPointsText != null && _walletData != null)
            {
                MetamaskPointsText.text = _walletData.bubbles.ToString();
            }
        }

        private void UpdateLeaderBoardData()
        {
            var formData = new GameFormData();
            formData.gameName = gameName;
            ServerService.PostDataToServer<PlayerApi>(PlayerApi.Leaderboard, JsonUtility.ToJson(formData), ((strRespo) =>
            {
                Debug.Log("Get Leaderboard response = " + strRespo);
                var resp = JsonUtility.FromJson<LeaderBoardResponseData>(strRespo);
                if (resp != null && resp.highScores != null && leaderboardContent != null)
                {
                    leaderboardContent.UpdateTopPlayers(resp.highScores);
                }
            }), ((strfailure) => {
                Debug.LogError("Leaderboard request failed");
            }));
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

            var enableCanvas = new CanvasGroup[] { UsernameOKButtonCanvasGroup, StartButtonCanvasGroup };
            if (!isGuest)
            {
                enableCanvas = enableCanvas.Append(MetamaskCanvasGroup).ToArray();
            }
            StartCoroutine(CanvasTransition(ConnectButtonCanvasGroup, 0.5f, true, () =>
            {

                StartCoroutine(CanvasTransition(enableCanvas, 0.5f, false));
            }));

            state = GameState.Connected;
            yield return null;
        }

        IEnumerator StartGameTransition()
        {
            startNewSession();
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
            UpdateLeaderBoardData();
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

        void startNewSession()
        {
            currentSessionId = "";
            if (_authData != null && !isGuest)
            {
                SessionStartData formData = new()
                {
                    signature = _authData.signature,
                    address = _authData.address,
                    mode = "FREE",
                    timezone = timeZone,
                    startTime = getTime(),
                    gameName = gameName,
                };
               
                ServerService.PostDataToServer<SessionApi>(SessionApi.Start, JsonUtility.ToJson(formData), (resp) =>
                {
                    Debug.Log("START SESSION SUCCESS resp = " + resp);
                    var sessionData = JsonUtility.FromJson<SessionData>(resp);
                    if (sessionData != null && sessionData.sessionId != null && sessionData.sessionId != "")
                    {
                        currentSessionId = sessionData.sessionId;
                    }
                    else
                    {
                        Debug.Log("Failed to parse session ID");
                    }
                }, (str) => {
                    Debug.Log("Session start failed = " + str);
                });
            }
        }

        void updateSession()
        {
            if (state == GameState.Playing && _authData != null && !isGuest && currentSessionId != "")
            {
                SessionUpdateData formData = new()
                {
                    signature = _authData.signature,
                    address = _authData.address,
                    sessionId = currentSessionId,
                    score = GameScore,
                };

                ServerService.PostDataToServer<SessionApi>(SessionApi.Update, JsonUtility.ToJson(formData), (resp) =>
                {
                    Debug.Log("UPDATE SESSION SUCCESS resp = " + resp);
                }, (str) => {
                    Debug.Log("Session Update failed");
                });
            }
        }

        void endSession(bool win)
        {
            _bubbleEarned = 0;
            if (_authData != null && !isGuest && currentSessionId != "")
            {
                SessionEndData formData = new()
                {
                    signature = _authData.signature,
                    address = _authData.address,
                    sessionId = currentSessionId,
                    score = GameScore,
                    status = win ? "WON" : "LOSE",
                    endTime = getTime(),
                };
                currentSessionId = "";
                ServerService.PostDataToServer<SessionApi>(SessionApi.End, JsonUtility.ToJson(formData), (resp) =>
                {
                    Debug.Log("END SESSION SUCCESS resp = " + resp);

                    var data = JsonUtility.FromJson<SessionEndResponseData>(resp);
                    if (data != null && data.bubbles != null)
                    {
                        _bubbleEarned = data.bubbles;
                    }
                }, (str) => {
                    Debug.Log("Session END failed");
                });
            }
        }

        void SynchronizeWallet()
        {
            if (_authData != null)
            {
                ServerService.GetDataFromServer<PlayerApi, WalletData, string>(PlayerApi.GetWallet, (walletData) =>
                {
                    if (walletData != null)
                    {
                        _walletData = walletData;
                        UpdateScoreAndUser();
                    }

                }, _authData.address);
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
                SynchronizeWallet();
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
                Debug.Log("Auth Data Parsed succesfuly");
                    _authData = authData;
                    ServerService.GetDataFromServer<PlayerApi, GetGeneralDataResponse, string>(PlayerApi.GetGeneralData, ((resp) =>
                    {
                         SynchronizeWallet();
                        if (resp.nickname == null || resp.nickname == "")
                        {
                            Debug.Log("nicname empty");
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
                               Debug.Log("CONNECTED SUCCESFULY");
                            OnConnected();
                        }
                    }), authData.address);
                    Debug.Log("Waiting for nickname...");
                }
                else
                {
                    Debug.Log("Auth Data empty...");
                }
            }
            
#endif
        }

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenMetamaskLogin();
#endif
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