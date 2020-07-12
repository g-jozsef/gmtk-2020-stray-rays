using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;

    public void SliderChanged(float newVal)
    {
        if (newVal < -9.7f)
        {
            _mixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            _mixer.SetFloat("MasterVolume", newVal);
        }
    }
}
