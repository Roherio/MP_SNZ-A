using System.Collections.Generic;
using UnityEngine;


//Aquest script fa el teletransport entre pantalles i el FadeIn i FadeOut
public class portalsScript : MonoBehaviour
{

    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject player;
    public static bool levelTransitioning = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || portalObjects.Contains(collision.gameObject)) return;

        if (destination.TryGetComponent(out portalsScript destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        var fade = Camera.main.GetComponent<CameraFading>();
        if (fade != null)
        {
            levelTransitioning = true;
            fade.FadeOut(() =>
            {
                Invoke("Teleport", 0);
                Invoke("LevelTransitioningFalse", 0.2f);
                fade.FadeIn(() =>
                {
                    //levelTransitioning = false;
                });
            });
        }
        else
        {
            player.transform.position = destination.position;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portalObjects.Remove(collision.gameObject);
        }
    }

    private void LevelTransitioningFalse()
    {
        levelTransitioning = false;
    }
    private void Teleport()
    {
        player.transform.position = destination.position;
    }
}