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
        LORE = default, MOVEMENT, PLATFORMS, WALLS, LIMITS, ENEMIES, PICK_UPS, COUNT
    }

    [Header("Tutorial UI")]
    [SerializeField] private GameObject nextStep;
    [SerializeField] private GameObject prevStep;
    [SerializeField] private TMP_Text stepText;
    [SerializeField] private TMP_Text titleText;
    [Header("Tutorial Steps")]
    [SerializeField] private GameObject stepFolder;
    [Header("Change scene")]
    [SerializeField] private string menuSceneName;

    public static Action<int> OnStepChange;

    private string[] titleArray = { "LORE", "MOVEMENT", "PLATFORMS", "WALLS", "LIMITS", "ENEMIES", "PICK UPS" };
    private CanvasGroup[] stepArray;

    //private Step currentStep;
    private int actualStep = 0;

    private void Awake()
    {
        stepText.text = actualStep.ToString();
        titleText.text = titleArray[(int)Step.LORE];
    }
    private void Start()
    {
        stepArray = stepFolder.GetComponentsInChildren<CanvasGroup>();

        for (int i = 0; i < stepArray.Length; i++)
        {
            stepArray[i].alpha = 0;
            stepArray[i].blocksRaycasts = false;
            stepArray[i].interactable = false;
        }

        stepArray[actualStep].alpha = 1;
        stepArray[actualStep].blocksRaycasts = true;
        stepArray[actualStep].interactable = true;
    }

    public void NextStep()
    {
        HideStep(actualStep);
        actualStep++;
        if (actualStep == (int)Step.COUNT-1)
        {
            nextStep.SetActive(false);
        }
        if (actualStep > 0)
        {
            prevStep.SetActive(true);
        }
        ShowStep(actualStep);
        stepText.text = actualStep.ToString();

        titleText.text = titleArray[actualStep];

        OnStepChange?.Invoke(actualStep);
    }
    public void PrevStep()
    {
        HideStep(actualStep);
        actualStep--;
        if (actualStep == 0)
        {
            prevStep.SetActive(false);
        }
        if (actualStep < (int)Step.COUNT-1)
        {
            nextStep.SetActive(true);
        }
        ShowStep(actualStep);
        stepText.text = actualStep.ToString();

        titleText.text = titleArray[actualStep];

        OnStepChange?.Invoke(actualStep);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    private void HideStep(int posToHide)
    {
        stepArray[posToHide].alpha = 0;
        stepArray[posToHide].blocksRaycasts = false;
        stepArray[posToHide].interactable = false;

    }
    private void ShowStep(int posToShow)
    {
        stepArray[posToShow].alpha = 1;
        stepArray[posToShow].blocksRaycasts = true;
        stepArray[posToShow].interactable = true;
    }

}
