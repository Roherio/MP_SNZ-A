using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
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
            audioSource.volume = AudioSettingsManager.instance.effectsVol;
        }
    }
}

