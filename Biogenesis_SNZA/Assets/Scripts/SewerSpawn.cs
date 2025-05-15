using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerSpawn : MonoBehaviour
{
    public GameObject DROP;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropSpawn());    
    }

    IEnumerator DropSpawn()
    {
        Instantiate(DROP,transform);
        yield return new WaitForSeconds(5);
        StartCoroutine(DropSpawn());
    }
}
