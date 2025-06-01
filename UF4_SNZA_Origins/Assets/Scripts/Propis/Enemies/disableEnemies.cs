using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROPI
//Script per fer que els enemics es desactivin
//AQUEST SCRIPT NO ES VALID PEL BOSS
public class disableEnemies : MonoBehaviour
{
    public GameObject Enemies;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Enemies.SetActive(!Enemies.activeSelf);
        }
    }


}
