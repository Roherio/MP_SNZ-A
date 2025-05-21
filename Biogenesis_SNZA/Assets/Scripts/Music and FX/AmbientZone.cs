using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AudioClip ambienceMusic;
    public float fadeDuration = 1f;

    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInside = true;
        MusicManager.Instance.PlayMusic(ambienceMusic);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (isPlayerInside)
        {
            isPlayerInside = false;
            MusicManager.Instance.StopMusic();
        }
    }
}