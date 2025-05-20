using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientZone : MonoBehaviour
{
    public float fadeDuration = 1f;

    private AudioSource ambientSource;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        ambientSource = GetComponent<AudioSource>();
        ambientSource.loop = true;
        ambientSource.playOnAwake = false;
        ambientSource.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeIn()
    {
        if (!ambientSource.isPlaying) ambientSource.Play();

        float time = 0f;
        while (time < fadeDuration)
        {
            ambientSource.volume = Mathf.Lerp(0f, 1f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        ambientSource.volume = 1f;
    }

    IEnumerator FadeOut()
    {
        float startVol = ambientSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            ambientSource.volume = Mathf.Lerp(startVol, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        ambientSource.volume = 0f;
        ambientSource.Stop();
    }
}