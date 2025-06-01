using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que controla quins sons pot fer el boss final quan interactua amb el jugador/món
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
