using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    #region VARIABLES
    #region SERIALIZED VARIABLES
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private CanvasGroup gameOverPanel;

    [Header("Menu scene")]
    [SerializeField] private string menuSceneName = "";
    #endregion

    #region STATIC VARIABLES

    #endregion

    #region PRIVATE VARIABLES
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
        PauseSystem.OnPauseStateChange += PausePanelController;
        GameManager.OnGameOver += GameOverPanelController;
    }
    private void OnDestroy()
    {
        PauseSystem.OnPauseStateChange -= PausePanelController;
        GameManager.OnGameOver -= GameOverPanelController;
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
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    #endregion
    #endregion
}

