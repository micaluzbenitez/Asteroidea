using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public enum Step
    {
        MOVEMENT = default, PLATFORMS, PICK_UPS, LIFE_BAR, EXPLAINATION, COUNT
    }

    [SerializeField] private GameObject nextStep;
    [SerializeField] private GameObject prevStep;
    [SerializeField] private TMP_Text stepText;
    public Step currentStep;

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
    }

}
