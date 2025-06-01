using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROPI
//Per com funciona el comportament del Bossfinal, necesitem que no estigui present fins que comenci l'enfrontament, per tant utilitzem aquest script per desactivar-lo al principi
public class disableUrsina : MonoBehaviour
{
    private hpEnemiesScript hpEnemiesScript;
    public GameObject Ursina;
    public GameObject UrsinaHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        hpEnemiesScript = GameObject.FindGameObjectWithTag("Ursina").GetComponent<hpEnemiesScript>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Liora_StateMachine_Script.isDying == true)
        {
            Invoke("lioraDefeated", 2f);
        }
    }
    public void lioraDefeated()
    {
        hpEnemiesScript.enemyHP = hpEnemiesScript.maxEnemyHP;
        gameObject.SetActive(false);
        UrsinaHealthBar.SetActive(false);
    }

}
