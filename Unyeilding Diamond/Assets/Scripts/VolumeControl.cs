using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer SFXMixer;
    public AudioMixer ambMixer;

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("volume", Mathf.Log10(volume) * 20);

    }
    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        
    }
    public void SetSFXVolume(float volume)
    {
        SFXMixer.SetFloat("volume", Mathf.Log10(volume) * 20);

    }
    public void SetAmbVolume(float volume)
    {
        ambMixer.SetFloat("volume", Mathf.Log10(volume) * 20);

    }
}
