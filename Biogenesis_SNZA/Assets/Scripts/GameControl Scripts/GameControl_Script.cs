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