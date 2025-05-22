using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossMusicController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip introClip;
    public AudioClip loopClip;
    public AudioClip endClip;

    private bool hasStarted = false;
    private bool hasStartedLoop = false;
    private bool hasPlayedEnd = false;
    public bool bossDefeated = false;

    private void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = audioSource.volume * AudioSettingsManager.instance.musicVol;
        // Start loop after intro ends
        if (hasStarted && !audioSource.isPlaying && !hasStartedLoop && !bossDefeated)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true;
            audioSource.Play();
            hasStartedLoop = true;
        }

        // Play end clip when boss is defeated
        if (bossDefeated && !hasPlayedEnd)
        {
            PlayEndMusic();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            StartMusic();
        }
    }

    public void StartMusic()
    {
        hasStarted = true;
        audioSource.clip = introClip;
        audioSource.loop = false;
        audioSource.Play();
        print("introBoss");
    }

    public void TriggerBossDefeat()
    {
        bossDefeated = true;
    }

    private void PlayEndMusic()
    {
        hasPlayedEnd = true;
        audioSource.loop = false;
        audioSource.clip = endClip;
        audioSource.Play();
    }
}