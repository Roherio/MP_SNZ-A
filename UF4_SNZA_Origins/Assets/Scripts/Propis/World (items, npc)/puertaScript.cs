using System.Collections;
using UnityEngine;
using Cinemachine;
public class puertaScript : MonoBehaviour, IInteractable_Script
{
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject player;
    [SerializeField] private PolygonCollider2D mapBoundary;
    private CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public bool CanInteract()
    {
        return true;
    }
    public void Interact()
    {
        StartCoroutine(FadeTeleport());
    }

    private IEnumerator FadeTeleport()
    {
        portalsScript.levelTransitioning = true;

        var fade = Camera.main.GetComponent<CameraFading>();
        if (fade != null)
        {
            fade.FadeOut(() =>
            {
                player.transform.position = destination.position;
                confiner.m_BoundingShape2D = mapBoundary;
                fade.FadeIn(() =>
                {
                    portalsScript.levelTransitioning = false;
                });
            });
        }
        else
        {
            player.transform.position = destination.position;
            confiner.m_BoundingShape2D = mapBoundary;
            portalsScript.levelTransitioning = false;
        }

        yield return null;
    }
}
