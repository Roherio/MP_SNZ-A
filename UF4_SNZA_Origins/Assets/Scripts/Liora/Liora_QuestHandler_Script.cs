using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_QuestHandler_Script : MonoBehaviour
{
    [SerializeField] public GameObject colliderArdilla;

    
    // Start is called before the first frame update
    void Start()
    {
        colliderArdilla.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventsManager_Script.poderKhione)
        {
            colliderArdilla.SetActive(true);
        }
    }
}
