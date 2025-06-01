using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script que controla quins sons fa l'enemic quan interactua amb el jugador/a
public class secretarioAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;



    public AudioClip playerDetected;
    public AudioClip Attack;
    public AudioClip takeDamage;
    public AudioClip Death;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SecretarioSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Update()
    {
        audioSource.volume = AudioSettingsManager.effectsVol;
    }
}

