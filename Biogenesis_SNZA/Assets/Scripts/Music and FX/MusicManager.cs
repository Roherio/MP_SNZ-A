using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSourceA;
    public AudioSource audioSourceB;

    private AudioSource currentSource;
    private AudioSource nextSource;

    public float fadeDuration = 2f;
    private Coroutine currentFade;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSourceA.loop = true;
        audioSourceB.loop = true;
        currentSource = audioSourceA;
        nextSource = audioSourceB;
    }

    public void PlayMusic(AudioClip newClip)
    {
        // Avoid restarting the same music
        if (currentSource.clip == newClip) return;

        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeToNewMusic(newClip));
    }

    public void StopMusic()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

             currentFade = StartCoroutine(FadeOutAndStop());
    }

    IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            currentSource.volume = (1f - t) * AudioSettingsManager.Instance.musicVol;
            nextSource.volume = t * AudioSettingsManager.Instance.musicVol;
            time += Time.deltaTime;
            yield return null;
        }

        currentSource.Stop();
        currentSource.volume = 0f;
        nextSource.volume = AudioSettingsManager.Instance.musicVol;

        // Swap references
        var temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;
    }
    IEnumerator FadeOutAndStop()
    {
        float time = 0f;
        float startVolumeA = audioSourceA.volume;
        float startVolumeB = audioSourceB.volume;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            audioSourceA.volume = Mathf.Lerp(startVolumeA, 0f, t) * AudioSettingsManager.Instance.musicVol;
            audioSourceB.volume = Mathf.Lerp(startVolumeB, 0f, t) * AudioSettingsManager.Instance.musicVol;
            time += Time.deltaTime;
            yield return null;
        }

        audioSourceA.Stop();
        audioSourceB.Stop();
        audioSourceA.clip = null;
        audioSourceB.clip = null;

        audioSourceA.volume = 1f;
        audioSourceB.volume = 1f;
    }
}
