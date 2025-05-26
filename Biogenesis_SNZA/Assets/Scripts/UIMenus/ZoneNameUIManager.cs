using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneNameUIManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static ZoneNameUIManager instance;

    public CanvasGroup canvasGroup;
    public Image zoneImage;        
    public float fadeDuration = 0.5f;
    public float displayTime = 2f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        canvasGroup.alpha = 0;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = AudioSettingsManager.effectsVol;
    }
    public void ShowZoneImage(Sprite zoneSprite)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        audioSource.Play();
        currentRoutine = StartCoroutine(FadeZoneImage(zoneSprite));
    }

    private IEnumerator FadeZoneImage(Sprite zoneSprite)
    {

        zoneImage.sprite = zoneSprite;

        // Fade in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(displayTime);

        // Fade out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}