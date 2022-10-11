using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Managers;

public class UIController : MonoBehaviour
{
    #region VARIABLES
    #region SERIALIZED VARIABLES
    [Header("Start panel")]
    [SerializeField] private CanvasGroup startingPanel;

    [Header("Pause panel")]
    [SerializeField] private CanvasGroup pausePanel;

    [Header("Game over panel")]
    [SerializeField] private CanvasGroup gameOverPanel;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text scoreText;

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
        }
        else
        {
            ShowPanel(pausePanel);
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
    private void ChangeEndGamePanelValues(int distance, int score)
    {
        scoreText.text = score.ToString();
        distanceText.text = distance.ToString();
    }
    #endregion
    #endregion
}

