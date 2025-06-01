using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script que mostra en quina zona del mapa es troba el jugador al canviar de zona
public class showTitle : MonoBehaviour
{
    public Sprite zoneImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ZoneNameUIManager.instance.ShowZoneImage(zoneImage);
        }
    }
}
