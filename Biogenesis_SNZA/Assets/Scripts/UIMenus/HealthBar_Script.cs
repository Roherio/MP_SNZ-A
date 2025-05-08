using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    //els dos sliders que mostren la vida a la UI
    public Slider healthSlider;
    public Slider healthSliderDelay;

    //[SerializeField] private float delayStart = 0.3f;
    [SerializeField] private float delaySpeed = 0.01f;
    
    void Start()
    {
        healthSlider.value = GameControl_Script.lifeLiora;
        healthSliderDelay.value = GameControl_Script.lifeLiora;
    }
    
    void Update()
    {
        if (healthSlider.value != GameControl_Script.lifeLiora)
        {
            healthSlider.value = GameControl_Script.lifeLiora;
        }
        if (healthSlider.value != healthSliderDelay.value)
        {
            //StartCoroutine(DelayHealth());
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, GameControl_Script.lifeLiora, delaySpeed);
        }
        /*if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.value != healthSliderDelay.value)
        {
            //StartCoroutine(DelayHealth());
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, currentHealth, delaySpeed);
        }*/
    }

    /*IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
    }*/
}