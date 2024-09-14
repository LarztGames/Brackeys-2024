using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnClick : MonoBehaviour
{
    public void Play(AudioClip audio) => SFXManager.instance.PlaySoundFXClip(audio, transform);
}
