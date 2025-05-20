using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrsinaAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;


    public AudioClip clawAttack;
    public AudioClip growl;
    public AudioClip Roar;
    public AudioClip groundStep;
    public AudioClip iceBreaker;
    public AudioClip Death;

    public void UrsinaSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
