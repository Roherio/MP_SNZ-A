using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//script PROPI
//configura el text que ha de mostrar la UI de la munició, agafant el valor de GameControl_Script.municion
public class MunicionText_Script : MonoBehaviour
{
    public TMP_Text textMunicion;

    // Update is called once per frame
    void Update()
    {
        textMunicion.text = GameControl_Script.municion.ToString();
    }
}
