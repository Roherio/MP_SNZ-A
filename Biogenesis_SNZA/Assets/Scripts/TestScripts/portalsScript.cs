using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalsScript : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject player;
    public static bool levelTransitioning = false;


    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }
        if (destination.TryGetComponent(out portalsScript destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        StartCoroutine(FadeTimer());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        portalObjects.Remove(collision.gameObject);
    }
    IEnumerator FadeTimer()
    {
        portalsScript.levelTransitioning = true;
        yield return new WaitForSeconds(1f);
        player.transform.position = destination.position;
        portalsScript.levelTransitioning = false;
    }
}
 