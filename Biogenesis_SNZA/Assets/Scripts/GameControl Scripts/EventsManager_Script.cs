using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager_Script : MonoBehaviour
{
    //script que pasará todo esto al SAVEDATA para guardarse
    
    //logica Recolectables misiones
    public static bool barraKhione = false;
    public static bool muelleKhione = false;
    public static bool taponesRumo = false;
    public static bool mantaRumo = false;
    //logica Poderes
    public static bool poderKhione = false;
    public static bool poderRumo = false;

    //eventosDialogo
    public static bool habladoGander1vez = false;
    public static bool habladoKhione1vez = false;
    public static bool habladoWallace1vez = false;
    public static bool habladoRumo1vez = false;
    public static bool habladoUrsina1vez = false;

    private void Awake()
    {
        BotonesActivos_Script.bObjKhione1 = barraKhione;
        BotonesActivos_Script.bObjKhione2 = muelleKhione;
        BotonesActivos_Script.bObjRumo1 = taponesRumo;
        BotonesActivos_Script.bObjRumo2 = mantaRumo;
        BotonesActivos_Script.bAnilloKhione = poderKhione;
        BotonesActivos_Script.bAnilloRumo = poderRumo;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //funciones para activar los botones de la ui
    //-------------------ObjMisiones
    public static void ActivarObjKhione1()
    {
        barraKhione = true;
        BotonesActivos_Script.bObjKhione1 = true;
    }
    public static void ActivarObjKhione2()
    {
        muelleKhione = true;
        BotonesActivos_Script.bObjKhione2 = true;
    }
    public static void ActivarObjRumo1()
    {
        taponesRumo = true;
        BotonesActivos_Script.bObjRumo1 = true;
    }
    public static void ActivarObjRumo2()
    {
        mantaRumo = true;
        BotonesActivos_Script.bObjRumo2 = true;
    }
    public static void DesactivarObjKhione1()
    {
        barraKhione = false;
        BotonesActivos_Script.bObjKhione1 = false;
    }
    public static void DesactivarObjKhione2()
    {
        muelleKhione = false;
        BotonesActivos_Script.bObjKhione2 = false;
    }
    public static void DesactivarObjRumo1()
    {
        taponesRumo = false;
        BotonesActivos_Script.bObjRumo1 = false;
    }
    public static void DesactivarObjRumo2()
    {
        mantaRumo = false;
        BotonesActivos_Script.bObjRumo2 = false;
    }
    //-------------------misiones
    public static void ActivarAnilloKhione()
    {
        poderKhione = true;
        BotonesActivos_Script.bAnilloKhione = true;
    }
    public static void ActivarAnilloRumo()
    {
        poderRumo = true;
        BotonesActivos_Script.bAnilloRumo = true;
    }
}
