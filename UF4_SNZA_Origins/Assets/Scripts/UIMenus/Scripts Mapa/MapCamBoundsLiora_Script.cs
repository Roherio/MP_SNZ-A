using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamBoundsLiora_Script : MonoBehaviour
{
    [SerializeField] private int maskIndexActivar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MapManager_Script.instance.ActivateMask(maskIndexActivar);
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MapManager_Script.instance.DeactivateMask(maskIndexActivar);
        }
    }*/
}
