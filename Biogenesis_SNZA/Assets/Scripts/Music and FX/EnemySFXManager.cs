using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (AudioSettingsManager.instance != null)
        {
            audioSource.volume = AudioSettingsManager.effectsVol;
        }
    }
}
