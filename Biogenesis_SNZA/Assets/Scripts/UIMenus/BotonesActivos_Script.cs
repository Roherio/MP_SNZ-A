using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesActivos_Script : MonoBehaviour
{
    //botones SNZAs
    //public GameObject SNZAJabali;
    //public GameObject SNZASecretario;

    //booleanas inventario
    public static bool bCristalizador = false;
    public static bool bCuero = false;
    public static bool bVial = false;
    public static bool bAnilloKhione = false;
    public static bool bAnilloRumo = false;
    public static bool bObjKhione1 = false;
    public static bool bObjKhione2 = false;
    public static bool bObjRumo1 = false;
    public static bool bObjRumo2 = false;
    //----------------------------------
    private bool bCristalizadorActivated = false;
    private bool bCueroActivated = false;
    private bool bVialActivated = false;
    private bool bAnilloKhioneActivated = false;
    private bool bAnilloRumoActivated = false;
    private bool bObjKhione1Activated = false;
    private bool bObjKhione2Activated = false;
    private bool bObjRumo1Activated = false;
    private bool bObjRumo2Activated = false;

    //botones Inventario
    public GameObject cristalizador;
    public GameObject cuero;
    public GameObject vial;
    public GameObject anilloKhione;
    public GameObject anilloRumo;
    public GameObject objKhione1;
    public GameObject objKhione2;
    public GameObject objRumo1;
    public GameObject objRumo2;

    private void Awake()
    {
        //iniciamos todos los objetos que puede ser que no tengamos en false, luego en Start se activarán si EventsManager se lo dice
        cristalizador.SetActive(false);
        cuero.SetActive(false);
        vial.SetActive(false);
        anilloKhione.SetActive(false);
        anilloRumo.SetActive(false);
        objKhione1.SetActive(false);
        objKhione2.SetActive(false);
        objRumo1.SetActive(false);
        objRumo2.SetActive(false);
        //SNZAJabali.SetActive(false);
        //SNZASecretario.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //inicialització activant botons si al Awake de EventsManager sabem que te els objectes
        if (bCristalizador)
        {
            bCristalizadorActivated = true;
            cristalizador.SetActive(true);
        }
        if (bCuero)
        {
            bCueroActivated = true;
            cuero.SetActive(true);
        }
        if (bVial)
        {
            bVialActivated = true;
            vial.SetActive(true);
        }
        if (bAnilloKhione)
        {
            bAnilloKhioneActivated = true;
            anilloKhione.SetActive(true);
        }
        if (bAnilloRumo)
        {
            bAnilloRumoActivated = true;
            anilloRumo.SetActive(true);
        }
        if (bObjKhione1)
        {
            bObjKhione1Activated = true;
            objKhione1.SetActive(true);
        }
        if (bObjKhione2)
        {
            bObjKhione2Activated = true;
            objKhione2.SetActive(true);
        }
        if (bObjRumo1)
        {
            bObjRumo1Activated = true;
            objRumo1.SetActive(true);
        }
        if (bObjRumo2)
        {
            bObjRumo2Activated = true;
            objRumo2.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //activar objetos
        if (bCristalizador && !bCristalizadorActivated)
        {
            bCristalizadorActivated = true;
            cristalizador.SetActive(true);
        }
        if (bCuero && !bCueroActivated)
        {
            bCueroActivated = true;
            cuero.SetActive(true);
        }
        if (bVial && !bVialActivated)
        {
            bVialActivated = true;
            vial.SetActive(true);
        }
        if (bAnilloKhione && !bAnilloKhioneActivated)
        {
            bAnilloKhioneActivated = true;
            anilloKhione.SetActive(true);
        }
        if (bAnilloRumo && !bAnilloRumoActivated)
        {
            bAnilloRumoActivated = true;
            anilloRumo.SetActive(true);
        }
        if (bObjKhione1 && !bObjKhione1Activated)
        {
            bObjKhione1Activated = true;
            objKhione1.SetActive(true);
        }
        if (bObjKhione2 && !bObjKhione2Activated)
        {
            bObjKhione2Activated = true;
            objKhione2.SetActive(true);
        }
        if (bObjRumo1 && !bObjRumo1Activated)
        {
            bObjRumo1Activated = true;
            objRumo1.SetActive(true);
        }
        if (bObjRumo2 && !bObjRumo2Activated)
        {
            bObjRumo2Activated = true;
            objRumo2.SetActive(true);
        }
        //desactivar objetos
        if (!bCristalizador && bCristalizadorActivated)
        {
            bCristalizadorActivated = false;
            cristalizador.SetActive(false);
        }
        if (!bCuero && bCueroActivated)
        {
            bCueroActivated = false;
            cuero.SetActive(false);
        }
        if (!bVial && bVialActivated)
        {
            bVialActivated = false;
            vial.SetActive(false);
        }
        if (!bObjKhione1 && bObjKhione1Activated)
        {
            objKhione1.SetActive(false);
        }
        if (!bObjKhione2 && bObjKhione2Activated)
        {
            objKhione2.SetActive(false);
        }
        if (!bObjRumo1 && bObjRumo1Activated)
        {
            objRumo1.SetActive(false);
        }
        if (!bObjRumo2 && bObjRumo2Activated)
        {
            objRumo2.SetActive(false);
        }
    }
}