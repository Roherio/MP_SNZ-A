using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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

