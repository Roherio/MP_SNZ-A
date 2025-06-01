using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

//PROPI
//L'unica funció d'aquest script es fer respawn dels enemics (si estan morts) un cop has descansat o es torna a carregar el joc (seguint l'exemple dels metroidvania i Soulslike)
public class ParentEnemy : MonoBehaviour
{
    hpEnemiesScript hpEnemiesScript;
    public GameObject Enemy;
    public bool enemyDead = false;

    public void Respawn()
    {
        if (enemyDead)
        {
            Instantiate(Enemy, transform);
            enemyDead = false;
        }
    }
}

