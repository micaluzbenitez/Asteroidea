using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Managers;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region VARIABLES
    #region SERIALIZED VARIABLES
    [Header("Start panel")]
    [SerializeField] private CanvasGroup startingPanel = null;

    [Header("Pause panel")]
    [SerializeField] private CanvasGroup pausePanel = null;
    [SerializeField] private TMP_Text pauseDistanceText = null;
    [SerializeField] private TMP_Text pauseScoreText = null;
    [SerializeField] private TMP_Text pauseCoinsText = null;

    [SerializeField] private GameObject pauseButton = null;
    [SerializeField] private Sprite pausedSprite = null;
    [SerializeField] private Sprite resumedSprite = null;

    [Header("Game over panel")]
    [SerializeField] private CanvasGroup gameOverPanel = null;
    [SerializeField] private TMP_Text distanceText = null;
    [SerializeField] private TMP_Text scoreText = null;
    [SerializeField] private TMP_Text coinsText = null;
    [SerializeField] private TMP_Text highscoreDistanceText = null;
    [SerializeField] private TMP_Text highscoreScoreText = null;

    [Header("Scene")]
    [SerializeField] private string menuSceneName = "";
    [SerializeField] private string gameSceneName = "";
    #endregion

    #region STATIC VARIABLES

    #endregion

    #region PRIVATE VARIABLES
    #endregion
    #endregion

    #region METHODS
    #region PUBLIC METHODS
    public void Replay()
    {
        SceneManager.LoadScene(gameSceneName);
    } 

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    #endregion

    #region STATIC METHODS

    #endregion

    #region PRIVATE METHODS
    private void Awake()
    {
        PauseSystem.OnPauseStateChange += PausePanelController;
        GameManager.OnGameOver += GameOverPanelController;
        GameManager.OnGameStart += GameStartPanelController;
        GameManager.OnEndGame += ChangeEndGamePanelValues;
    }
    private void OnDestroy()
    {
        PauseSystem.OnPauseStateChange -= PausePanelController;
        GameManager.OnGameOver -= GameOverPanelController;
        GameManager.OnGameStart -= GameStartPanelController;
        GameManager.OnEndGame -= ChangeEndGamePanelValues;
    }
    private void PausePanelController(PauseStates state)
    {
        if (state == PauseStates.Resumed)
        {
            HidePanel(pausePanel); //if it is resumed, then, i should show the message
            pauseButton.GetComponent<Image>().sprite = resumedSprite;
        }
        else
        {
            pauseCoinsText.text = coinsText.text;
            pauseDistanceText.text = distanceText.text;
            pauseScoreText.text = scoreText.text;
            ShowPanel(pausePanel);
            pauseButton.GetComponent<Image>().sprite = pausedSprite;
        }
    }
    private void GameOverPanelController()
    {
        ShowPanel(gameOverPanel);
    }
    private void GameStartPanelController()
    {
        HidePanel(startingPanel);
    }

    private void ShowPanel(CanvasGroup panel)
    {
        panel.alpha = 1;
        panel.blocksRaycasts = true;
        panel.interactable = true;
    }
    private void HidePanel(CanvasGroup panel)
    {
        panel.blocksRaycasts = false;
        panel.interactable = false;
        panel.alpha = 0;
    }
    private void ChangeEndGamePanelValues(int distance, int score, int highscoreDistance, int highscoreScore)
    {
        scoreText.text = score.ToString();
        distanceText.text = distance.ToString() + " m";
        highscoreScoreText.text = highscoreScore.ToString();
        highscoreDistanceText.text = highscoreDistance.ToString() + " m";
    }
    #endregion
    #endregion
}