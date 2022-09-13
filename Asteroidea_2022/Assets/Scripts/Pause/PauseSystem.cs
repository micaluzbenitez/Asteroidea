using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;


public class PauseSystem : MonoBehaviour
{
    [SerializeField] private bool startInPauseState = false;

    public static Action<PauseStates> OnPauseStateChange;
    public static bool Paused { get; private set; }
    private void Awake()
    {
        InputManager.OnPausePress += PauseControl;
        GameManager.OnGameOver += Pause;
    }
    private void OnDestroy()
    {
        InputManager.OnPausePress -= PauseControl;
        GameManager.OnGameOver -= Pause;
    }

    private void Start()
    {
        Paused = !startInPauseState;
        PauseControl();
    }

    public static void PauseControl()
    {
        if (GameManager.GameRunning)
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
            PauseStates actualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
            Debug.Log("Envio estado de pausa: " + Paused);
            OnPauseStateChange?.Invoke(actualState);
        }
    }

    public static void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
    }
    public static void UnPause()
    {
        Paused = false;
        Time.timeScale = 1;
    }

}
