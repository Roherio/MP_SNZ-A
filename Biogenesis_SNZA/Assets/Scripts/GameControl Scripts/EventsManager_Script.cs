using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager_Script : MonoBehaviour
{
    //logica Recolectables
    public static int piezasKhione = 0;
    public static bool cascosRumo = false;
    public static bool mantaRumo = false;
    //logica Poderes
    public static bool poderKhione = false;
    private bool anilloKhioneActivated = false;
    public static bool poderRumo = false;
    private bool anilloRumoActivated = false;

    //eventosTienda
    public static bool habladoKhione1vez = false;

    // Start is called before the first frame update
    void Start()
    {
        if (poderKhione == true)
        {
            //BotonesActivos_Script.ActivarAnilloKhione();
        }
        if (poderRumo == true)
        {
            //BotonesActivos_Script.ActivarAnilloRumo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (poderKhione && !anilloKhioneActivated)
        {
            print("el poder de khione ta activau");
            anilloKhioneActivated = true;
            //BotonesActivos_Script.anilloKhione.SetActive(true);
        }
        if (poderRumo && !anilloRumoActivated)
        {
            anilloRumoActivated = true;
            //BotonesActivos_Script.anilloKhione.SetActive(true);
        }
    }

    //funciones para activar los botones de la ui
    public void ActivarAnilloKhione()
    {
        poderKhione = true;
        //BotonesActivos_Script.anilloKhione.SetActive(true);
    }
}
