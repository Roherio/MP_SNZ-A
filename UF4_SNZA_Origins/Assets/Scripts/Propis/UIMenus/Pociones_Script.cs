using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pociones_Script : MonoBehaviour
{
    //script PROPI
    //script que configura cada poció instanciada individualment, controlant si està activa o està utilitzada i quin sprite ha de mostrar
    public Sprite spriteNuevo;
    public Sprite spriteUsado;
    public bool pocionActiva = true;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = spriteNuevo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PotionUsed()
    {
        pocionActiva = false;
        image.sprite = spriteUsado;
    }
    public void PotionRecovered()
    {
        pocionActiva = true;
        image.sprite = spriteNuevo;
    }
}
