using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROPI
//El nom d'aquests cript pot ser confus, ja que sembla un general, pero realment només s'utilitza per les gotes que fan mal que cauen desde el sostre en la zona 2 del joc
//Té aquest nom perque va ser el primer script per so que vam crear
public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource SFXSource;

    [Header ("Sound Effects")]
    public AudioClip sewerDropSpawn;

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
