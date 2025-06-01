using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que controla quins sons fa el jugador quan fem alguna acció/interactua amb l'entorn
public class LioraAudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource audioSource;


    public AudioClip shorSlash;
    public AudioClip voiceShortSlash;
    public AudioClip longSLash;
    public AudioClip voiceLongSlash;

    public AudioClip jabaliAttack;
    public AudioClip voiceLightSmash;
    public AudioClip voiceHeavySmash;


    public AudioClip secretarioAttack;


    public AudioClip perfectParry;

    public AudioClip dash;
    public AudioClip voiceDash;

    public AudioClip jump;

    public AudioClip damage;
    public AudioClip voiceDamage;

    public void LioraSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}