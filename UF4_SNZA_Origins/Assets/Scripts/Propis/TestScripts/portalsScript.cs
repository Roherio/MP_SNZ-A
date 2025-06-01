using System.Collections.Generic;
using UnityEngine;

public class portalsScript : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    public GameObject rumoDestroyFinishMission;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject player;
    [SerializeField] private int indexDesbloquearMapa;
    public static bool levelTransitioning = false;

    private void Start()
    {
        //rumoDestroyFinishMission = GameObject.FindObjectOfType<NPCRumo_Script>().gameObject;
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
                //MapManager_Script.instance.ActivateMap(indexDesbloquearMapa);
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
        if (EventsManager_Script.poderRumo)
        {
            Destroy(rumoDestroyFinishMission);
        }
    }
    private void Teleport()
    {
        player.transform.position = destination.position;
    }
}