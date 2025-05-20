using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SNZAProgressControl_Script : MonoBehaviour
{
    public static bool bKilledJabali = false;
    public static bool bKilledSecretario = false;

    [Header("JABALI")]
    public static float progressAmountJabali;
    public static bool jabaliCristalizable = false;
    [Header("SECRETARIO")]
    public static float progressAmountSecretario;
    public static bool secretarioCristalizable = false;

    public static bool snzaCangrejoConseguida = true;
    public static float progresoSNZAJabali = 150f;
    public static bool snzaJabaliConseguida = false;
    public static float progresoSNZASecretario = 150f;
    public static bool snzaSecretarioConseguida = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bKilledJabali || Input.GetKeyDown(KeyCode.P))
        {
            //aqui SI QUE ENTRA
            //if (progressAmountJabali <= 0) { return; }
            if (progresoSNZAJabali <= 0) { return; }
            ProgresarSNZAJabali(10);
            bKilledJabali = false;
        }
        if (bKilledSecretario || Input.GetKeyDown(KeyCode.O))
        {
            //if (progressAmountSecretario <= 0) { return; }
            if (progresoSNZASecretario <= 0) { return; }
            ProgresarSNZASecretario(10);
            bKilledSecretario = false;
        }
    }
    void ProgresarSNZAJabali(float cantidad)
    {
        //progressAmountJabali -= cantidad;
        progresoSNZAJabali -= cantidad;
        bKilledJabali = false;
        if (progressAmountJabali <= 0)
        {
            print("hasmatao un jabali");
            jabaliCristalizable = true;
        }
    }
    void ProgresarSNZASecretario(float cantidad)
    {
        //progressAmountSecretario -= cantidad;
        progresoSNZASecretario -= cantidad;
        bKilledSecretario = false;
        if (progressAmountSecretario <= 0)
        {
            print("hasmatao un secretario");
            secretarioCristalizable = true;
        }
    }
}
