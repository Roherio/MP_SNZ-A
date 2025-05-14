using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesActivos_Script : MonoBehaviour
{
    //botones SNZAs
    //public GameObject SNZAJabali;
    //public GameObject SNZASecretario;

    //botones Inventario
    public GameObject mapa;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        print(anilloKhione.activeSelf);
    }

    //funciones de activar/desactivar botones
    //--------------------------------------SNZAs
    public void ActivarSNZAJabali()
    {
        //SNZAJabali.SetActive(true);
    }
    public void ActivarSNZASecretario()
    {
        //SNZASecretario.SetActive(true);
    }
    //--------------------------------------consumibles (activar y desactivar, ya que si no tenemos de estos en nuestro inventario, deberían desaparecer)
    public void ActivarCristalizador()
    {
        cristalizador.SetActive(true);
    }
    public void ActivarCuero()
    {
        cuero.SetActive(true);
    }
    public void ActivarVial()
    {
        vial.SetActive(true);
    }
    public void DesactivarCristalizador()
    {
        cristalizador.SetActive(false);
    }
    public void DesactivarCuero()
    {
        cuero.SetActive(false);
    }
    public void DesactivarVial()
    {
        vial.SetActive(false);
    }
    //--------------------------------------misiones
    public void ActivarObjKhione1()
    {
        objKhione1.SetActive(true);
    }
    public void ActivarObjKhione2()
    {
        objKhione2.SetActive(true);
    }
    public void ActivarObjRumo1()
    {
        objRumo1.SetActive(true);
    }
    public void ActivarObjRumo2()
    {
        objRumo2.SetActive(true);
    }
    //--------------------------------------poderes
    public void ActivarAnilloKhione()
    {
        anilloKhione.SetActive(true);
    }
    public void ActivarAnilloRumo()
    {
        anilloRumo.SetActive(true);
    }
}
