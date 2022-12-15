using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRTPC : MonoBehaviour

{
    [SerializeField] AK.Wwise.RTPC RTPCToBeChanged = new AK.Wwise.RTPC();

    private Slider slider = null;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateRTPC()
    {
        RTPCToBeChanged.SetGlobalValue(slider.value);
    }
}