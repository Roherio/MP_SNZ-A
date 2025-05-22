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
            fadeCanvasGroup.alpha = 0;
    }

    public void FadeOut(Action onComplete)
    {
        if (!isFading)
            StartCoroutine(Fade(1f, onComplete));
    }

    public void FadeIn(Action onComplete)
    {
        if (!isFading)
            StartCoroutine(Fade(0f, onComplete));
    }

    private IEnumerator Fade(float targetAlpha, Action onComplete)
    {
        isFading = true;
        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        isFading = false;
        onComplete?.Invoke();
    }
}