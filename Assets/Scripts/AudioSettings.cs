using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider SoundSlider;
    public Slider MusicSlider;
    public AudioMixer mixer;
    void Start()
    {
        float db;
        if(mixer.GetFloat("Sound", out db))
            SoundSlider.value = (db + 80) / 80;
        if(mixer.GetFloat("Music", out db))
            MusicSlider.value = (db + 80) / 80;
    }
    public void Sound(float value)
    {
        value = value*80 - 80;
        mixer.SetFloat("Sound", value);
    }
    public void Music(float value)
    {
        value = value*80 - 80;
        mixer.SetFloat("Music", value);
    }
}
