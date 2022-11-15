using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Collections;
using Entities.Enemies;
using Toolbox.Lerpers;
using Entities.Player;
using UnityEngine.UI;
using UnityEngine;
using Toolbox;
using System;
using TMPro;
using UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [Header("Player")]
        [SerializeField] private PlayerStats playerStats = null;

        [Header("Enemies")]
        [SerializeField] private EnemiesManager enemiesManager = null;

        [Header("Pickups")]
        [SerializeField] private PickupsManager pickupsManager = null;

        [Header("Bonus Level")]
        [SerializeField] private ParticleSystem bonusLevel = null;
        [SerializeField] private float timePerBonusLevel = 50;

        [Header("UI")]
        [SerializeField] private UIGame uiGame = null;
        [SerializeField] private TMP_Text timerText = null;
        [SerializeField] private Button pauseButton = null;
        [SerializeField] private int timerStartingValue = 5;

        [Header("Player")]
        [SerializeField] private FloatingJoystick joystick = null;

        [Header("Skins")]
        [SerializeField] private SpriteRenderer player = null;
        [SerializeField] private SpriteRenderer[] feets = null;
        [SerializeField] private Animator[] feetsAnimator = null;
        [SerializeField] private Skin[] skins = null;

        [Header("Camera Movement")]
        [SerializeField] private float startingVerticalSpeed = 1.0f;
        [SerializeField] private float timeForNextAugment = 5;
        [SerializeField] private float augmentDuration = 3;
        [SerializeField] private float augmentValue = 0.001f;
        [SerializeField] private float maxVerticalSpeed = 6;

        [Header("Background")]
        [SerializeField] private SpriteRenderer background = null;
        [SerializeField] private Color initColor = Color.white;
        [SerializeField] private Color finalColor = Color.white;
        [SerializeField] private float colorChangeSpeed = 0;

        [Header("Lights")]
        [SerializeField] private Light2D globalLight = null;
        [SerializeField] private Light2D playerLight = null;
        [SerializeField] private float distanceLightReduction = 0;
        [SerializeField] private float lightChangeSpeed = 0;
        [SerializeField] private float lightReductionValue = 0;
        #endregion

        #region STATIC VARIABLES
        public static bool GameRunning { get; private set; } = true;

        public static Action OnGameOver;
        public static Action OnGameStart;
        public static Action<int, int, int, int> OnEndGame; //Time, Score

        public static float VerticalSpeed
        {
            get
            {
                return verticalSpeed;
            }
            private set
            {
                verticalSpeed = value;
            }
        }
        #endregion

        #region PRIVATE VARIABLES
        private bool gameOver = false;
        private bool skippedTimer = false;
        private bool gameStarted = false;

        private float time = 0;
        private int pickupPoints = 0;
        private float points = 0;
        private int coins = 0;
        
        private int distance = 0;
        private int score = 0;
        private int higshcoreDistance = 0;
        private int higshcoreScore = 0;

        private Timer timer = new Timer();
        private int timerTime = 0;

        private static float verticalSpeed = 0;
        private static float verticalMaxSpeed = 0;

        private ColorLerper backgroundLerper = new ColorLerper();
        private FloatLerper lightLerper = new FloatLerper();
        private bool lightOn = true;
        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            PlayerStats.OnUpdateLife += uiGame.UpdateLifeBar;
            PlayerStats.OnAddScore += AddScore;
            PlayerEnemies.OnLoseLife += playerStats.LoseLife;
            DeathChecker.OnReachLimit += EndGame;
            InputManager.OnJumpPress += SkipTimer;
            joystick.OnTap += SkipTimer;

            timer.SetTimer(timerStartingValue, Timer.TIMER_MODE.DECREASE);
            backgroundLerper.SetValues(initColor, finalColor, colorChangeSpeed, Lerper<Color>.LERPER_TYPE.STEP_SMOOTH, true);

            timer.OnReachedTime += StartGame;
            verticalMaxSpeed = maxVerticalSpeed;

            SetPlayerSkin();
        }

        private IEnumerator Start()
        {
            for (int i = 0; i < enemiesManager.transform.childCount; i++)
            {
                Jellyfish mine = enemiesManager.transform.GetChild(i).GetComponent<Jellyfish>();
                if (mine) mine.SetTarget(playerStats.transform);
            }

            gameOver = false;
            GameRunning = true;

            PauseSystem.Pause();

            yield return null;

            timerTime = timerStartingValue;

            timer.ActiveTimer();

            VerticalSpeed = startingVerticalSpeed;
        }

        private void Update()
        {
            // Init game
            if (timer.Active)
            {
                
                timer.UpdateTimer();
                ChangeTimerText();
            }

            // Game
            uiGame.UpdateGameData((int)distance, score);

            // Score
            points += Time.deltaTime;
            score = (int)points + pickupPoints;

            // Distance
            time += Time.deltaTime * verticalSpeed;
            distance = (int)time;

            // Background color
            UpdateBackgroundColor();

            // Light reduction
            if (distance > distanceLightReduction) TurnOffLight();
            ChangeLight();

            // Bonus level
            if (distance % timePerBonusLevel == 0) BonusLevel();
        }

        private void OnDestroy()
        {
            PlayerStats.OnUpdateLife -= uiGame.UpdateLifeBar;
            PlayerStats.OnAddScore -= AddScore;
            PlayerEnemies.OnLoseLife -= playerStats.LoseLife;
            DeathChecker.OnReachLimit -= EndGame;
            //InputManager.OnJumpPress -= SkipTimer;
            if (!skippedTimer) timer.OnReachedTime -= StartGame;
            if (!gameStarted) InputManager.OnJumpPress -= SkipTimer;
        }

        private void SetPlayerSkin()
        {
            for (int i = 0; i < skins.Length; i++)
            {
                if (PlayerPrefs.GetInt("Skin") == skins[i].ID)
                {
                    player.sprite = skins[i].body;

                    for (int j = 0; j < feets.Length; j++)
                    {
                        feetsAnimator[j].enabled = false;
                        feets[j].sprite = skins[i].feet;
                        skins[i].SetAnimation(feetsAnimator[j]);
                    }
                }
            }
        }

        private void StartGame()
        {
            for (int j = 0; j < feets.Length; j++)
            {
                feetsAnimator[j].enabled = true;
            }

            PauseSystem.UnPause();
            OnGameStart?.Invoke();
            gameStarted = true;
            InputManager.OnJumpPress -= SkipTimer;
            joystick.OnTap -= SkipTimer;
            StartCoroutine(SecondsTimer());
            pauseButton.gameObject.SetActive(true);
        }

        private void SkipTimer()
        {
            timer.OnReachedTime -= StartGame;
            skippedTimer = true;
            StartGame();
        }

        private void ChangeTimerText()
        {
            int timeElapsed = Mathf.RoundToInt(timer.CurrentTime);
            if (timeElapsed < 1)
                timerText.text = "GO!";
            else if (timeElapsed < 6)
                timerText.text = timeElapsed.ToString();
            else
                timerText.text = (timerStartingValue - 1).ToString();
        }

        private void UpdateBackgroundColor()
        {
            if (backgroundLerper.Active)
            {
                backgroundLerper.UpdateLerper();
                background.color = backgroundLerper.GetValue();
            }
        }

        private void TurnOnfLight()
        {
            if (!lightOn)
            {
                lightLerper.SetLerperValues(lightReductionValue, 1, lightChangeSpeed, Lerper<float>.LERPER_TYPE.STEP_SMOOTH, true);
                lightOn = true;
            }
        }

        private void TurnOffLight()
        {
            if (lightOn)
            {
                lightLerper.SetLerperValues(1, lightReductionValue, lightChangeSpeed, Lerper<float>.LERPER_TYPE.STEP_SMOOTH, true);
                lightOn = false;
            }
        }

        private void ChangeLight()
        {
            if (lightLerper.Active)
            {
                lightLerper.UpdateLerper();
                globalLight.intensity = lightLerper.GetValue();
                playerLight.intensity = 1 - lightLerper.GetValue();
            }
        }

        private void AddScore(int score)
        {
            pickupPoints += score;
        }

        private void BonusLevel()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();   

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].gameObject.SetActive(false);
                Instantiate(bonusLevel, enemies[i].transform.position, Quaternion.identity);
                pickupsManager.SpawnRandomPickup(enemies[i].transform.position);
            }
        }

        private void EndGame()
        {
            pauseButton.gameObject.SetActive(false);
            uiGame.SetLifeBarValue(0);
            gameOver = true;
            GameRunning = false;
            StopCoroutine(SpeedAugment());
            OnGameOver?.Invoke();

            SaveHighscore("Distance", distance, ref higshcoreDistance);
            SaveHighscore("Score", score, ref higshcoreScore);
            OnEndGame?.Invoke(distance, score, higshcoreDistance, higshcoreScore);
        }

        private void SaveHighscore(string typeOfData, int data, ref int highscore)
        {
            if (PlayerPrefs.GetInt(typeOfData) < data)
            {
                highscore = data;
                PlayerPrefs.SetInt(typeOfData, data);
            }
            else
            {
                highscore = PlayerPrefs.GetInt(typeOfData);
            }
        }

        private IEnumerator SecondsTimer()
        {
            yield return new WaitForSeconds(timeForNextAugment);
            StartCoroutine(SpeedAugment());
        }

        private IEnumerator SpeedAugment()
        {
            float t = 0;
            while (t < augmentDuration)
            {
                t += Time.deltaTime;
                verticalSpeed += augmentValue * Time.deltaTime;
                yield return null;
            }
            if (verticalSpeed + augmentValue < maxVerticalSpeed)
            {
                StartCoroutine(SecondsTimer());
            }
        }

        public static float GetMaxVerticalSpeed()
        {
            return verticalMaxSpeed;
        }

        #endregion
        #endregion
    }
}