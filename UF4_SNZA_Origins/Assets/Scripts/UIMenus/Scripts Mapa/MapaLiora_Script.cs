using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapaLiora_Script : MonoBehaviour
{
    //script nomes per fer parpadear la icona de liora al mapa
    private float timer = 0f;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.75f)
        {
            image.enabled = !image.enabled;
            timer = 0f;
        }
    }
}