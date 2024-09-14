using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; set; }

    [SerializeField]
    private AudioSource soundFXObject;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    public void PlaySoundFXClip(
        AudioClip audioClip,
        Transform spawnTransform,
        float volume = 1,
        float duration = 1
    )
    {
        AudioSource audioSource = Instantiate(
            soundFXObject,
            spawnTransform.position,
            Quaternion.identity
        );

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = (duration == -1) ? audioSource.clip.length : duration;
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource PlaySoundFXClipReturned(
        AudioClip audioClip,
        Transform spawnTransform,
        float volume = 1,
        float duration = 1
    )
    {
        AudioSource audioSource = Instantiate(
            soundFXObject,
            spawnTransform.position,
            Quaternion.identity
        );

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        return audioSource;
    }

    internal void PlaySoundFXClip(object audioClip, Transform transform)
    {
        throw new NotImplementedException();
    }
}
