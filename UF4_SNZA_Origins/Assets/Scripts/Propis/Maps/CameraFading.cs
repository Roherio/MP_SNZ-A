using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Unity.VisualScripting;


//PROPI
//script per fer que la camera faci un FadeIn/FadeOut mitjançant funcions que cridarem en altres scripts

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
        isFading = true; //Fem aquest boolean per tal que no hi hagi overlap de Fades en cas que un jugador passi massa rapid entre portals/zones
        float startAlpha = fadeCanvasGroup.alpha; //guardem el valor actual de l'alpha del CanvasGroup
        float time = 0f; //creem una funcio time per fer un timer

        while (time < duration) //la variable duration es porta desde un altre script
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration); //Mathf.Lerp el que fa es fer una "transicio" desde un valor A (startAlpha) fins a un valor B (targetAlpha) 
            //durant un temps t (Que en el nostre cas es time entre la duracio que portem desde la funcio de d'alt, encara que podriem fer que es porti desde un altre script, pero aixi ho tenim controlat desde un sol lloc)
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        isFading = false;
        onComplete?.Invoke(); //Aixo el que fa es cridar l'accio OnComplete, en el nostre cas no hem fet res cap dels cop que l'hem cridat, pero per exemple si quan cridem la funcio fem aixo:
        //StartCoroutine(Fade(1f, () => print("Fade completado!"), 1f));
        //En la consola veuriem que fica "Fade completado", també amb la part de "() =>" indiquem que la funcio no rep cap parametre
    }


    //Aquestes funcions nomes son per quan volem un efecte més dramatic
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