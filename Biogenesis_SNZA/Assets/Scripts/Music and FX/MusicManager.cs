using System.Collections;
using UnityEngine;

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
            ZoneNameUIManager.instance.ShowZoneImage(zoneImage);
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
