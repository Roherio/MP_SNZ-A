using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_Script : MonoBehaviour
{
    public static GameControl_Script instance;
    
    public static float lifeLiora = 100f;
    public static float maxLife = 100f;
    public static float adrenalineLiora = 0f;
    public static int moneyLiora = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        //si no existeix un SaveFile, farem que la vida de la liora s'inicialitzi sent la m�xima. Del contrari, l'agafa del save file. farem el mateix per totes les variables que cal guardar del gameControl.
        if (!FindObjectOfType<SaveController_Script>().HasSaveFile())
        {
            lifeLiora = maxLife;
        }
        /*if (SaveController_Script.saveLocation == null)
        {
            lifeLiora = maxLife;
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float hpLiora, float damageEnemy)
    {
        hpLiora -= damageEnemy;
    }
    public void EnemyTakeDamage(float hpEnemy, float damageLiora)
    {

    }
}