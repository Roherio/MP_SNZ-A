using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableUrsina : MonoBehaviour
{
    private hpEnemiesScript hpEnemiesScript;
    public GameObject Ursina;
    private Transform UrsinaLocation;
    public GameObject UrsinaHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        UrsinaLocation = Ursina.transform;
        hpEnemiesScript = GameObject.FindGameObjectWithTag("Ursina").GetComponent<hpEnemiesScript>();
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hpEnemiesScript.enemyHP = hpEnemiesScript.maxEnemyHP;
        Ursina.transform.position = UrsinaLocation.position;
        gameObject.SetActive(false);
        UrsinaHealthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
