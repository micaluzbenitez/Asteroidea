using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStepSelector : MonoBehaviour
{
    [Header("Initial Step")]
    [SerializeField] private TutorialManager.Step initialStep = default;
    
    [Header("Step Prefabs")]
    [SerializeField] private List<GameObject> stepPrefabs = null;

    [Header("Joystick Reference")]
    [SerializeField] private Joystick joystick = null;

    private int actualStep = 0;
    private GameObject expositor = null;

    private void Awake()
    {
        TutorialManager.OnStepChange += ChangeEnvironment;
    }

    private void OnDestroy()
    {
        TutorialManager.OnStepChange -= ChangeEnvironment;
    }

    private void Start()
    {
        expositor = Instantiate(stepPrefabs[actualStep], transform);
    }

    private void ChangeEnvironment(int step)
    {
        Destroy(expositor);
        actualStep = step;
        expositor = Instantiate(stepPrefabs[actualStep], transform);
        expositor.GetComponentInChildren<Entities.Player.PlayerMovement>().SetJoystick(joystick);
    }

}
