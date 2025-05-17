using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretarioAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;


    public AudioClip playerDetected;
    public AudioClip Attack;
    public AudioClip takeDamage;
    public AudioClip Death;

    public void SecretarioSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

