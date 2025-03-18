using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMemoria : MonoBehaviour
{

    public static scrMemoria Instance;

    public static int memIdBotonActivo = navMenuIG.idBotonActivo;
    public static int memIdSubBotonActivo = navMenuIG.idSubBotonActivo;
    public static int memMoney = navMenuIG.money, memLife = navMenuIG.life, memAdrenaline = navMenuIG.adrenaline;

    public static int[] intsMemoria = { memLife, memMoney, memAdrenaline };

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
        memIdSubBotonActivo = navMenuIG.idSubBotonActivo;
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
