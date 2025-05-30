using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CameraFading : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private bool isFading = false;

    private void Start()
    {
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 1;
    }

    public void FadeOut(Action onComplete)
    {
        if (!isFading)
            StartCoroutine(Fade(1f, onComplete, 1f));
    }

    public void FadeIn(Action onComplete)
    {
        if (!isFading)
            StartCoroutine(Fade(0f, onComplete, 1f));
    }

    private IEnumerator Fade(float targetAlpha, Action onComplete, float duration)
    {
        isFading = true;
        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        isFading = false;
        onComplete?.Invoke();
    }

    public void FadeInSlow(Action onComplete = null)
    {
        if (!isFading)
            StartCoroutine(Fade(0f, onComplete, 1.5f));
    }
    public void FadeOutSlow(Action onComplete = null)
    {
        if (!isFading)
            StartCoroutine(Fade(1f, onComplete, 1.5f));
    }
}