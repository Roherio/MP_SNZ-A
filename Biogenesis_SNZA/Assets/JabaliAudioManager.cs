using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabaliAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;


    public AudioClip playerDetected;
    public AudioClip Attack;
    public AudioClip takeDamage;
    public AudioClip Death;

    public void JabaliSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
