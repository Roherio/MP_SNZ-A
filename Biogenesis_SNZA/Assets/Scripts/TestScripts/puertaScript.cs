using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class puertaScript : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] GameObject player;
    [SerializeField] GameObject InteractButton;
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner confiner;
    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("On TriggerBox");
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(FadeTimer());
        }

    }
    public void Teleport()
    {
        player.transform.position = destination.position;
    }

    IEnumerator FadeTimer()
    {
        portalsScript.levelTransitioning = true;
        yield return new WaitForSeconds(.5f);
        Teleport();
        confiner.m_BoundingShape2D = mapBoundary;
        yield return new WaitForSeconds(0.5f);
        portalsScript.levelTransitioning = false;
    }
}

