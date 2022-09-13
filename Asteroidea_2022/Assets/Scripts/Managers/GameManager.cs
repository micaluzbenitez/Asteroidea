using System;
using UnityEngine;
using Entities.Player;
using UI;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    #region SERIALIZED VARIABLES
    [Header("Player")]
    [SerializeField] private PlayerStats playerStats = null;
    [SerializeField] private PlayerEnemies playerEnemies = null;

    [Header("UI")]
    [SerializeField] private UIGame uiGame = null;
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
        PlayerStats.OnLoseLife += uiGame.UpdateLifeBar;
        DeathChecker.OnReachLimit += EndGame;
    }
    private void OnDestroy()
    {
        PlayerStats.OnLoseLife -= uiGame.UpdateLifeBar;
        DeathChecker.OnReachLimit -= EndGame;
    }

    private void Start()
    {
        gameOver = false;
        GameRunning = true;
    }
    private void OnEnable()
    {
        playerEnemies.OnLoseLife += playerStats.LoseLife;
    }

    private void OnDisable()
    {
        playerEnemies.OnLoseLife -= playerStats.LoseLife;
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

