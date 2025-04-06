using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    //els dos sliders que mostren la vida a la UI
    public Slider healthSlider;
    public Slider healthSliderDelay;

    //cuando antes la vida la determinaba este script, fase de pruebas.
    /*public float maxHealth = 100f;
    public static float currentHealth;*/

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

        //comprobacion de que recibe daño, fase de pruebas.
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(10);
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(10);
        }

        if (healthSlider.value != healthSliderDelay.value)
        {
            //StartCoroutine(DelayHealth());
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, currentHealth, delaySpeed);
        }*/
    }
    void TakeDamage(float damage)
    {
        GameControl_Script.lifeLiora -= damage;
        //currentHealth -= damage;
    }

    /*IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
    }*/
}