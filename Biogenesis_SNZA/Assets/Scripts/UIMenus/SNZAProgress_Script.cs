using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SNZAProgress_Script : MonoBehaviour
{
    public GameObject tachadoCristalizador;
    public static bool bTachadoCristalizador;
    private bool cristalizadorTachado = false;

    //variable que nos permitirá ser llamada desde otro script como true para entrar en la funcion de progresar SNZA
    public static bool bKilledJabali = false;
    public static bool bKilledSecretario = false;

    //jabali
    public GameObject progresoJabali;
    public Image progressBarJabali;
    private float progressAmountJabali;
    public TextMeshProUGUI enemiesLeftJabali;
    public GameObject tachadoJabali;
    
    /*
    //secretario
    public GameObject progresoSecretario;
    public Image progressBarSecretario;
    private float progressAmountSecretario;*/
    // Start is called before the first frame update
    void Start()
    {
        tachadoJabali.SetActive(false);
        tachadoCristalizador.SetActive(false);
        progressAmountJabali = GameControl_Script.progresoSNZAJabali;
        progressBarJabali.fillAmount = (150 - progressAmountJabali) / 150f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (bKilledJabali || Input.GetKeyDown(KeyCode.P))
        {
            if (progressAmountJabali <= 0) { return; }
            ProgresarSNZAJabali(10);
            bKilledJabali = false;
        }
        /*if (bKilledSecretario || Input.GetKeyDown(KeyCode.O))
        {
            if (progressAmountSecretario <= 0) { return; }
            ProgresarSNZASecretario(10);
            bKilledSecretario = false;
        }*/
        if (bTachadoCristalizador && !cristalizadorTachado)
        {
            tachadoCristalizador.SetActive(true);
            cristalizadorTachado = true;
        }
        if (!bTachadoCristalizador && cristalizadorTachado)
        {
            tachadoCristalizador.SetActive(false);
            cristalizadorTachado = false;
        }

    }
    void ProgresarSNZAJabali(float cantidad)
    {
        progressAmountJabali -= cantidad;
        enemiesLeftJabali.text = "x" + (progressAmountJabali / 10);
        progressBarJabali.fillAmount = (150 - progressAmountJabali) / 150f;
        if (progressAmountJabali <= 0)
        {
            tachadoJabali.SetActive(true);
        }
    }
    /*void ProgresarSNZASecretario(float cantidad)
    {
        progressAmountSecretario -= cantidad;
        enemiesLeftSecretario.text = "x" + (progressAmountSecretario / 10);
        progressBarSecretario.fillAmount = (150 - progressAmountSecretario) / 150f;
        if (progressAmountSecretario <= 0)
        {
            tachadoSecretario.SetActive(true);
        }
    }*/
}