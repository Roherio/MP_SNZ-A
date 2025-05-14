using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            currentSource.volume = 1f - t;
            nextSource.volume = t;
            time += Time.deltaTime;
            yield return null;
        }

        currentSource.Stop();
        currentSource.volume = 1f;
        nextSource.volume = 1f;

        // Swap references
        var temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;
    }
}
