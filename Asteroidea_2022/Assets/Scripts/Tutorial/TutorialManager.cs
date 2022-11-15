using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public enum Step
    {
        LORE = default, MOVEMENT, PLATFORMS, UI, WALLS, LIMITS, ENEMIES, PICK_UPS, COUNT
    }

    [Header("Tutorial UI")]
    [SerializeField] private GameObject nextStep;
    [SerializeField] private GameObject prevStep;
    [SerializeField] private TMP_Text stepText;
    [Header("Change scene")]
    [SerializeField] private string menuSceneName;

    public static Action<Step> OnStepChange;

    private Step currentStep;
    private int actualStep = 0;

    private void Awake()
    {
        stepText.text = actualStep.ToString();
    }

    public void NextStep()
    {
        actualStep++;
        if (actualStep == (int)Step.COUNT)
        {
            nextStep.SetActive(false);
        }
        if (actualStep > 0)
        {
            prevStep.SetActive(true);
        }
        stepText.text = actualStep.ToString();

        OnStepChange?.Invoke((Step)actualStep);
    }
    public void PrevStep()
    {
        actualStep--;
        if (actualStep == 0)
        {
            prevStep.SetActive(false);
        }
        if (actualStep < (int)Step.COUNT)
        {
            nextStep.SetActive(true);
        }
        stepText.text = actualStep.ToString();

        OnStepChange?.Invoke((Step)actualStep);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

}
