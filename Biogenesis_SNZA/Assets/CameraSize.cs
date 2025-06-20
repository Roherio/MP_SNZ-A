using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("changeCameraSize", 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            resetCameraSize();
        }
    }

    private void changeCameraSize()
    {
        vcam.m_Lens.OrthographicSize = 15f;
    }

    private void resetCameraSize()
    {
        vcam.m_Lens.OrthographicSize = 11f;
    }
}
