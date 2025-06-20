using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PROPI
//Script que fa que les llums facin l'efecte de "flicker" com que parpadejen
public class LightFLickering : MonoBehaviour
{

    Transform mainLight;
    Transform flickerLight;
    UnityEngine.Rendering.Universal.Light2D mainLightComponent;
    UnityEngine.Rendering.Universal.Light2D flickerLightComponent;


    // Start is called before the first frame update
    void Start()
    {
        mainLight = this.transform.GetChild(0);
        flickerLight = this.transform.GetChild(1);
        mainLightComponent = mainLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        flickerLightComponent = flickerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; ) //this is while(true)
        {
            float randomIntensity = Random.Range(1.5f, 3f);
            flickerLightComponent.intensity = randomIntensity;


            float randomTime = Random.Range(0f, 0.1f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
