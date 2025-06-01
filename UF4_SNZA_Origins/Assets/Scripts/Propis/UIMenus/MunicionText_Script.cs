using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MunicionText_Script : MonoBehaviour
{
    public TMP_Text textMunicion;

    // Update is called once per frame
    void Update()
    {
        textMunicion.text = GameControl_Script.municion.ToString();
    }
}
