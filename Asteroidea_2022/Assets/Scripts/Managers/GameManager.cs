using System.Collections.Generic;
using System.Collections;
using Entities.Enemies;
using Entities.Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Toolbox;
using Toolbox.Lerpers;
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

        [Header("UI")]
        [SerializeField] private UIGame uiGame = null;
        [SerializeField] private TMP_Text timerText = null;
        [SerializeField] private int timerStartingValue = 5;

        [Header("Camera Movement")]
        [SerializeField] private float startingVerticalSpeed = 1.0f;
        [SerializeField] private float timeForNextAugment = 5;
        [SerializeField] private float augmentDuration = 3;
        [SerializeField] private float augmentValue = 0.001f;
        [SerializeField] private float maxVerticalSpeed = 6;

        [Header("Lights")]
        [SerializeField] private Light2D globalLight = null;
        [SerializeField] private Light2D playerLight = null;
        [SerializeField] private float distanceLightReduction = 0;
        [SerializeField] private float lightChangeSpeed = 0;
        #endregion

        #region STATIC VARIABLES
        public static bool GameRunning { get; private set; } = true;

        public static Action OnGameOver;
        public static Action OnGameStart;
        public static Action<int,int> OnEndGame; //Time, Score

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
        private int distance = 0;

        private int pickupPoints = 0;
        private float points = 0;
        private int score = 0;

        private Timer timer = new Timer();
        private int timerTime = 0;

        private static float verticalSpeed = 0;
        private static float verticalMaxSpeed = 0;

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

            timer.SetTimer(timerStartingValue, Timer.TIMER_MODE.DECREASE);

            timer.OnReachedTime += StartGame;
            verticalMaxSpeed = maxVerticalSpeed;
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
            if (timer.Active)
            {
                timer.UpdateTimer();
                ChangeTimerText();
            }

            uiGame.UpdateGameData((int)distance, score);

            // Score
            points += Time.deltaTime;
            score = (int)points + pickupPoints;

            // Distance
            time += Time.deltaTime * verticalSpeed;
            distance = (int)time;

            // Light reduction
            if (distance > distanceLightReduction) TurnOffLight();
            ChangeLight();
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

        private void StartGame()
        {
            PauseSystem.UnPause();
            OnGameStart?.Invoke();
            gameStarted = true;
            InputManager.OnJumpPress -= SkipTimer;
            StartCoroutine(SecondsTimer());
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

        private void TurnOffLight()
        {
            if (lightOn)
            {
                lightLerper.SetLerperValues(0, 1, lightChangeSpeed, Lerper<float>.LERPER_TYPE.STEP_SMOOTH, true);
                lightOn = false;
            }
        }

        private void TurnOnLight()
        {
            if (!lightOn)
            {
                lightLerper.SetLerperValues(1, 0, lightChangeSpeed, Lerper<float>.LERPER_TYPE.STEP_SMOOTH, true);
                lightOn = true;
            }
        }

        private void ChangeLight()
        {
            if (lightLerper.Active)
            {
                lightLerper.UpdateLerper();
                globalLight.intensity = 1 - lightLerper.GetValue();
                playerLight.intensity = lightLerper.GetValue();
            }
        }

        private void AddScore(int score)
        {
            pickupPoints += score;
        }

        private void EndGame()
        {
            uiGame.SetLifeBarValue(0);
            gameOver = true;
            GameRunning = false;
            StopCoroutine(SpeedAugment());
            OnGameOver?.Invoke();
            OnEndGame?.Invoke((int)distance, score);
        }

        IEnumerator SecondsTimer()
        {
            yield return new WaitForSeconds(timeForNextAugment);
            StartCoroutine(SpeedAugment());
        }

        IEnumerator SpeedAugment()
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