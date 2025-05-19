using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SNZAProgressJabali_Script : MonoBehaviour
{
    //variable que nos permitirá ser llamada desde otro script como true para entrar en la funcion de progresar SNZA
    public static bool bKilledJabali = false;

    [Header("JABALI")]
    public GameObject RESTANTEJabali;
    public Image progressBarJabali;
    private float progressAmountJabali;
    public TextMeshProUGUI enemiesLeftJabali;
    public GameObject tachadoJabali;
    public bool jabaliCristalizable = false;
    public TextMeshProUGUI textoFinalJabali;

    public GameObject tachadoCristalizadorJabali;
    public static bool bTachadoCristalizadorJabali;
    private bool cristalizadorTachadoJabali = false;

    // Start is called before the first frame update
    void Start()
    {
        tachadoJabali.SetActive(false);
        tachadoCristalizadorJabali.SetActive(false);
        progressAmountJabali = SNZAProgressControl_Script.progresoSNZAJabali;
        progressBarJabali.fillAmount = (150 - progressAmountJabali) / 150f;
        textoFinalJabali.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        progressAmountJabali = SNZAProgressControl_Script.progressAmountJabali;
        jabaliCristalizable = SNZAProgressControl_Script.jabaliCristalizable;
        bKilledJabali = SNZAProgressControl_Script.bKilledJabali;

        enemiesLeftJabali.text = "x" + (progressAmountJabali / 10);
        progressBarJabali.fillAmount = (150 - progressAmountJabali) / 150f;
        if (progressAmountJabali <= 0)
        {
            tachadoJabali.SetActive(true);
        }

        /*if (bKilledJabali || Input.GetKeyDown(KeyCode.P))
        {
            if (progressAmountJabali <= 0) { return; }
            bKilledJabali = false;
        }*/
        if (bTachadoCristalizadorJabali && !cristalizadorTachadoJabali)
        {
            tachadoCristalizadorJabali.SetActive(true);
            cristalizadorTachadoJabali = true;
        }
        if (!bTachadoCristalizadorJabali && cristalizadorTachadoJabali)
        {
            tachadoCristalizadorJabali.SetActive(false);
            cristalizadorTachadoJabali = false;
        }

    }
    /*void ProgresarSNZAJabali(float cantidad)
    {
        progressAmountJabali -= cantidad;
        enemiesLeftJabali.text = "x" + (progressAmountJabali / 10);
        progressBarJabali.fillAmount = (150 - progressAmountJabali) / 150f;
        if (progressAmountJabali <= 0)
        {
            jabaliCristalizable = true;
            tachadoJabali.SetActive(true);
        }
    }*/
    public void DestruirJabali()
    {
        Destroy(RESTANTEJabali);
    }
}