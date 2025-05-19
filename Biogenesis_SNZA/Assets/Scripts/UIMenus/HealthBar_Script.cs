using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    //els dos sliders que mostren la vida a la UI
    public Slider healthSlider;
    public Slider healthSliderDelay;

    private float delayStart = 1f;
    [SerializeField] private float delaySpeed = 0.01f;
    
    void Start()
    {
        //entre 5 porque tiene 500
        healthSlider.value = (GameControl_Script.lifeLiora / 5);
        healthSliderDelay.value = (GameControl_Script.lifeLiora / 5);
    }
    
    void Update()
    {
        if (healthSlider.value != (GameControl_Script.lifeLiora / 5))
        {
            healthSlider.value = (GameControl_Script.lifeLiora / 5);
        }
        if (healthSlider.value != healthSliderDelay.value)
        {
            //StartCoroutine(DelayHealth());
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, (GameControl_Script.lifeLiora / 5), delaySpeed);
        }
    }
    /*
    IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
        healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, GameControl_Script.lifeLiora, delaySpeed);
    }*/
}