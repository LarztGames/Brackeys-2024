using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("Master", level);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFX", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music", level);
    }
}
