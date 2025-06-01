using System.Collections;
using UnityEngine;

//PROPI
//Aquest script només serveix per controlar la música del joc, tant per controlar quan comencen com el seu volum ( encara que el volum es controla desde un altre script)
public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Sprite zoneImage;
    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
            ZoneNameUIManager.instance.ShowZoneImage(zoneImage); //també fa que quan entres a una zona/nivell nou, surti un titol
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (AudioSettingsManager.instance != null)
        {
            audioSource.volume = AudioSettingsManager.musicVol;
        }
    }



}
