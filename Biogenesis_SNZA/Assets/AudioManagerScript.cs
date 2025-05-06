using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundMusic;

    [Header("Liora")]
    public AudioClip pasosLiora;
    public AudioClip dashLiora;
    public AudioClip recogerObjetoLiora;
    public AudioClip escalarLiora;
    public AudioClip parryLiora;
    public AudioClip hurtLiora;
    public AudioClip muerteLiora;

   

    public AudioClip ataqueCangrejoLiora;
    public AudioClip ataqueFuerteCangrejoLiora;

    public AudioClip ataqueJabaliLiora;
    public AudioClip ataqueFuerteJabaliLiora;

    public AudioClip ataqueSecretarioLiora;
    public AudioClip ataqueFuerteSecretarioLiora;

    public AudioClip ataqueEscarabajoLiora;
    public AudioClip ataqueFuerteEscarabajoLiora;



    [Header("Jabali")]
    public AudioClip deteccionJabali;
    public AudioClip ataqueJabali;
    public AudioClip hurtJabali;
    public AudioClip muerteJabali;

    [Header("Secretario")]
    public AudioClip deteccionSecretario;
    public AudioClip ataqueSecretario;
    public AudioClip hurtSecretario;
    public AudioClip muerteSecretario;

    [Header("Escarabajo")]
    public AudioClip deteccionEscarabajo;
    public AudioClip ataqueEscarabajo;
    public AudioClip hurtEscarabajo;
    public AudioClip muerteEscarabajo;

    [Header("Ursina")]
    public AudioClip ataqueGarraUrsina;
    public AudioClip ataqueSmashUrsina;
    public AudioClip ataqueGritoUrsina;
    public AudioClip ataquePinchosHelados;
    public AudioClip ataquePicosHelados;
    public AudioClip muerteUrsina;
}
