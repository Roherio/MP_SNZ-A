using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SNZAProgressSecretario_Script : MonoBehaviour
{
    //variable que nos permitirá ser llamada desde otro script como true para entrar en la funcion de progresar SNZA
    public static bool bKilledSecretario = false;

    [Header("SECRETARIO")]
    public GameObject RESTANTESecretario;
    public Image progressBarSecretario;
    private float progressAmountSecretario;
    public TextMeshProUGUI enemiesLeftSecretario;
    public GameObject tachadoSecretario;
    public bool secretarioCristalizable = false;
    public TextMeshProUGUI textoFinalSecretario;

    public GameObject tachadoCristalizadorSecretario;
    public static bool bTachadoCristalizadorSecretario;
    private bool cristalizadorTachadoSecretario = false;

    // Start is called before the first frame update
    void Start()
    {
        tachadoSecretario.SetActive(false);
        tachadoCristalizadorSecretario.SetActive(false);
        progressAmountSecretario = GameControl_Script.progresoSNZASecretario;
        progressBarSecretario.fillAmount = (150 - progressAmountSecretario) / 150f;
        textoFinalSecretario.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        progressAmountSecretario = SNZAProgressControl_Script.progressAmountSecretario;
        secretarioCristalizable = SNZAProgressControl_Script.secretarioCristalizable;
        bKilledSecretario = SNZAProgressControl_Script.bKilledSecretario;

        enemiesLeftSecretario.text = "x" + (progressAmountSecretario / 10);
        progressBarSecretario.fillAmount = (150 - progressAmountSecretario) / 150f;
        if (progressAmountSecretario <= 0)
        {
            tachadoSecretario.SetActive(true);
        }

        /*if (bKilledSecretario || Input.GetKeyDown(KeyCode.P))
        {
            if (progressAmountSecretario <= 0) { return; }
            bKilledSecretario = false;
        }*/
        if (bTachadoCristalizadorSecretario && !cristalizadorTachadoSecretario)
        {
            tachadoCristalizadorSecretario.SetActive(true);
            cristalizadorTachadoSecretario = true;
        }
        if (!bTachadoCristalizadorSecretario && cristalizadorTachadoSecretario)
        {
            tachadoCristalizadorSecretario.SetActive(false);
            cristalizadorTachadoSecretario = false;
        }

        

    }
    /*void ProgresarSNZASecretario(float cantidad)
    {
        progressAmountSecretario -= cantidad;
        enemiesLeftSecretario.text = "x" + (progressAmountSecretario / 10);
        progressBarSecretario.fillAmount = (150 - progressAmountSecretario) / 150f;
        if (progressAmountSecretario <= 0)
        {
            secretarioCristalizable = true;
            tachadoSecretario.SetActive(true);
        }
    }*/
    public void DestruirSecretario()
    {
        Destroy(RESTANTESecretario);
    }
}