using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNZAProgressBar_Script : MonoBehaviour
{
    //jabali
    public GameObject progresoJabali;
    public Image progressBarJabali;
    private float progressAmountJabali;
    /*
    //secretario
    public GameObject progresoSecretario;
    public Image progressBarSecretario;
    private float progressAmountSecretario;*/
    // Start is called before the first frame update
    void Start()
    {
        progressAmountJabali = GameControl_Script.progresoSNZAJabali;
        progressBarJabali.fillAmount = progressAmountJabali / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ProgresarSNZAJabali(10);
        }
    }
    void ProgresarSNZAJabali(float cantidad)
    {
        progressAmountJabali += cantidad;
        progressBarJabali.fillAmount = progressAmountJabali / 100f;
    }
    /*void ProgresarSNZASecretario(float cantidad)
    {
        progressAmountSecretario += cantidad;
        progressBarSecretario.fillAmount = progressAmountSecretario / 100f;
    }*/
}