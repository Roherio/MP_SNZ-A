using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMemoria : MonoBehaviour
{

    public static scrMemoria Instance;

    public static int memIdBotonActivo = navMenuIG.idBotonActivo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
