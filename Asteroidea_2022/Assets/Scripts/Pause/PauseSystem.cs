using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Managers;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private bool startInPauseState = false;

    public static Action<PauseStates> OnPauseStateChange;
    public static bool Paused { get; private set; }

    public static PauseStates ActualState { get; private set; }


    private void Awake()
    {
        InputManager.OnPausePress += PauseControl;
        GameManager.OnGameOver += Pause;

        Paused = !startInPauseState;
        ActualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
        PauseControl();

    }
    private void OnDestroy()
    {
        InputManager.OnPausePress -= PauseControl;
        GameManager.OnGameOver -= Pause;
    }

    public static void PauseControl()
    {
        if (GameManager.GameRunning)
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
            ActualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
            //Debug.Log("Envio estado de pausa: " + Paused);
            OnPauseStateChange?.Invoke(ActualState);
        }
    }

    public static void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
        ActualState = PauseStates.Paused;
    }

    public static void UnPause()
    {
        Paused = false;
        Time.timeScale = 1;
        ActualState = PauseStates.Resumed;
    }

}
