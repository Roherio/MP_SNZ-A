using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aquest script controla cada quan s'instancia una gota
public class SewerSpawn : MonoBehaviour
{
    public GameObject DROP;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(DropSpawn());    
    }

    IEnumerator DropSpawn()
    {
        Instantiate(DROP,transform);
        audio.Play();
        yield return new WaitForSeconds(Random.Range(2f,6f));
        StartCoroutine(DropSpawn());
    }
}
