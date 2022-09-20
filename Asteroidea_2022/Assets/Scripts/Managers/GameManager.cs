using System;
using UnityEngine;
using Entities.Player;
using Entities.Enemies;
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

        private float time = 0;
        private int score = 0;
        #endregion

        #region STATIC VARIABLES
        public static bool GameRunning { get; private set; } = true;

        public static Action OnGameOver;

        #endregion

        #region PRIVATE VARIABLES

        private bool gameOver = false;

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
        }

        private void Update()
        {
            time += Time.deltaTime * scoreSpeed;
            score = (int)time;

            uiGame.UpdateScore(score);
        }

        private void Start()
        {
            for (int i = 0; i < enemiesManager.transform.childCount; i++)
            {
                Mine mine = enemiesManager.transform.GetChild(i).GetComponent<Mine>();
                if (mine) mine.SetTarget(playerStats.transform);
            }

            gameOver = false;
            GameRunning = true;
        }

        private void OnDestroy()
        {
            PlayerStats.OnUpdateLife -= uiGame.UpdateLifeBar;
            PlayerEnemies.OnLoseLife -= playerStats.LoseLife;
            DeathChecker.OnReachLimit -= EndGame;
        }

        private void EndGame()
        {
            gameOver = true;
            GameRunning = false;
            OnGameOver?.Invoke();
        }

        #endregion
        #endregion
    }
}