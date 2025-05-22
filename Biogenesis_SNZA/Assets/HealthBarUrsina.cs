using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUrsina : MonoBehaviour
{
    //els dos sliders que mostren la vida a la UI
    public Slider healthSlider;
    public Slider healthSliderDelay;

    public float hpUrsina;
    private hpEnemiesScript hpEnemiesScript;

    private float delayStart = 1f;
    [SerializeField] private float delaySpeed = 0.01f;

    private void Awake()
    {
        hpEnemiesScript = GameObject.FindGameObjectWithTag("Ursina").GetComponent<hpEnemiesScript>();
        //gameObject.SetActive(false);
    }
    void Start()
    {
        //entre 5 porque tiene 500
        healthSlider.value = (hpUrsina / 10);
        healthSliderDelay.value = (hpUrsina / 10);
    }

    void Update()
    {
        hpUrsina = hpEnemiesScript.enemyHP;
        if (healthSlider.value != (hpUrsina / 10))
        {
            healthSlider.value = (hpUrsina / 10);
        }
        if (healthSlider.value != healthSliderDelay.value)
        {
            //StartCoroutine(DelayHealth());
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, (hpUrsina / 10), delaySpeed);
        }
    }
    /*
    IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
        healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, GameControl_Script.lifeLiora, delaySpeed);
    }*/
}
