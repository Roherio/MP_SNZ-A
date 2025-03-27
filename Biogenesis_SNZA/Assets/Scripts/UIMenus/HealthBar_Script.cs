using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    public Slider healthSlider;
    public Slider healthSliderDelay;

    public float maxHealth = 100f;
    public float currentHealth;

    //[SerializeField] private float delayStart = 0.3f;
    [SerializeField] private float delaySpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        healthSliderDelay.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != currentHealth)
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
        }
    }
    void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    /*IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
    }*/
}