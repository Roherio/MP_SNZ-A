using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
