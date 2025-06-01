using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTimer());
        }
    }
    IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(1f);
        confiner.m_BoundingShape2D = mapBoundary;
        yield return new WaitForSeconds(1f);
    }

}
