using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using UnityEngine.UI;


public class sfxSlide : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    public void VolumeSlider(float volume)
    {
        AkSoundEngine.SetRTPCValue("SFXvolume", volume);

    }
}