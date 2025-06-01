using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Esto sirve para cambiar el tamaño de la camara cuando entramos en la sala de la jefa final
public class CameraSize : MonoBehaviour
{
    CinemachineVirtualCamera vcam;


    //Por si acaso nos olvidamos de poner la camara
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
