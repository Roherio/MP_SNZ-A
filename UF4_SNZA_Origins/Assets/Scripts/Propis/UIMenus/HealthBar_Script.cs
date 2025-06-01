using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    //script PROPI
    //script que modifica visualment com es veu la vida a la HUD del joc. L'hem fet principalment per controlar com baixa la vida (fent slice a una imatge a mesura que et treuen vida) i per fer una barra secundària i controlar com de ràpid baixa despres de que et treuen vida, per mostrarla amb un efecte retardat que ens agradava
    //els dos sliders que mostren la vida a la UI
    public Slider healthSlider;
    public Slider healthSliderDelay;

    private float delayStart = 1f;
    [SerializeField] private float delaySpeed = 0.01f;
    
    void Start()
    {
        healthSlider.value = (GameControl_Script.lifeLiora);
        healthSliderDelay.value = (GameControl_Script.lifeLiora);
    }
    
    void Update()
    {
        if (healthSlider.value != (GameControl_Script.lifeLiora))
        {
            healthSlider.value = (GameControl_Script.lifeLiora);
        }
        if (healthSlider.value != healthSliderDelay.value)
        {
            healthSliderDelay.value = Mathf.Lerp(healthSliderDelay.value, (GameControl_Script.lifeLiora), delaySpeed);
        }
    }
}