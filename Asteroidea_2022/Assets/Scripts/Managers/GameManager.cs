using System.Collections.Generic;
using System.Collections;
using Entities.Enemies;
using Entities.Player;
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
        [Header("Score")]
        [SerializeField] private float scoreSpeed = 0;

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
        private int score = 0;

        private int timerTime = 0;

        private Timer timer = new Timer();
        private float realTimer = 0;

        private static float verticalSpeed = 0;
        private static float verticalMaxSpeed = 0;

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
                Mine mine = enemiesManager.transform.GetChild(i).GetComponent<Mine>();
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

            time += Time.deltaTime * scoreSpeed; //Para hacer que el score aumente con el tiempo de caida, multiplicar por verticalSpeed
            score = (int)time;

            realTimer += Time.deltaTime;

            uiGame.UpdateScore(score);

        }

        private void OnDestroy()
        {
            PlayerStats.OnUpdateLife -= uiGame.UpdateLifeBar;
            PlayerEnemies.OnLoseLife -= playerStats.LoseLife;
            DeathChecker.OnReachLimit -= EndGame;
            //InputManager.OnJumpPress -= SkipTimer;
            if (!skippedTimer) timer.OnReachedTime -= StartGame;
            if (!gameStarted) InputManager.OnJumpPress -= SkipTimer;
        }

        private void EndGame()
        {
            uiGame.SetLifeBarValue(0);
            gameOver = true;
            GameRunning = false;
            StopCoroutine(SpeedAugment());
            OnGameOver?.Invoke();
            OnEndGame?.Invoke((int)realTimer,(int)score);
        }

        private void ChangeTimerText()
        {
            int timeElapsed = Mathf.RoundToInt(timer.CurrentTime);
            if (timeElapsed < 1)
                timerText.text = "GO!";
            else if (timeElapsed < 6)
                timerText.text = timeElapsed.ToString();
            else 
                timerText.text = (timerStartingValue-1).ToString();
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