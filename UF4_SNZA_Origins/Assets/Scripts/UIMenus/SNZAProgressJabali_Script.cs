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
    //public TextMeshPro enemiesLeftJabali;
    public GameObject tachadoJabali;
    public static bool jabaliCristalizable = false;
    public TextMeshPro textoFinalJabali;

    public GameObject tachadoCristalizadorJabali;
    public static bool bTachadoCristalizadorJabali;
    private bool cristalizadorTachadoJabali = false;

    public GameObject recuadroCristalizable;

    // Start is called before the first frame update
    void Start()
    {
        textoFinalJabali.gameObject.SetActive(true);
        /*tachadoJabali.SetActive(false);
        tachadoCristalizadorJabali.SetActive(false);
        
        //progressAmountJabali = SNZAProgressControl_Script.progresoSNZAJabali;
        progressBarJabali.fillAmount = (150 - SNZAProgressControl_Script.progresoSNZAJabali) / 150f;
        //enemiesLeftJabali.text = "x" + (SNZAProgressControl_Script.progresoSNZAJabali / 10);
        recuadroCristalizable.SetActive(false);

        textoFinalJabali.gameObject.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*if (SNZAProgressControl_Script.snzaJabaliConseguida)
        {
            DestruirJabali();
        }
        //progressAmountJabali = SNZAProgressControl_Script.progresoSNZAJabali;
        jabaliCristalizable = SNZAProgressControl_Script.jabaliCristalizable;
        bKilledJabali = SNZAProgressControl_Script.bKilledJabali;

        //enemiesLeftJabali.text = "x" + (SNZAProgressControl_Script.progresoSNZAJabali / 10);
        progressBarJabali.fillAmount = (150 - SNZAProgressControl_Script.progresoSNZAJabali) / 150f;
        if (SNZAProgressControl_Script.progresoSNZAJabali <= 0)
        {
            tachadoJabali.SetActive(true);
        }

        if (bTachadoCristalizadorJabali && !cristalizadorTachadoJabali)
        {
            tachadoCristalizadorJabali.SetActive(true);
            cristalizadorTachadoJabali = true;
        }
        if (!bTachadoCristalizadorJabali && cristalizadorTachadoJabali)
        {
            tachadoCristalizadorJabali.SetActive(false);
            cristalizadorTachadoJabali = false;
        }*/

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