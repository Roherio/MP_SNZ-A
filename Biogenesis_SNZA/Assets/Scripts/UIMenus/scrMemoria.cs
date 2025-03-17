using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMemoria : MonoBehaviour
{

    public static scrMemoria Instance;

    public static int memIdBotonActivo = navMenuIG.idBotonActivo;
    public static int money, life, adrenaline;

    public static int[] variables = { money, life, adrenaline };

    // Start is called before the first frame update
    void Start()
    {
        variables[0] = money;
        variables[1] = life;
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarValores();
    }
    public static void GuardarIdBotonActivo()
    {
        memIdBotonActivo = navMenuIG.idBotonActivo;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void ActualizarValores()
    {
        variables[0] = money;
        variables[1] = life;
    }
}
