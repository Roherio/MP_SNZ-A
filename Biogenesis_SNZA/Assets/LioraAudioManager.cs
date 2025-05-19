using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LioraAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;


    public AudioClip shorSlash;
    public AudioClip voiceShortSlash;
    public AudioClip longSLash;
    public AudioClip voiceLongSlash;

    public AudioClip dash;
    public AudioClip voiceDash;

    public AudioClip damage;

    public void UrsinaSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}