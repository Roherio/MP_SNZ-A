using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;

        // Intentamos obtener la cámara virtual desde este objeto
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (CinemachineVirtualCamera == null)
        {
            CinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        var noise = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise != null)
        {
            noise.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }
        else
        {
            Debug.LogWarning("No se encontró el componente CinemachineBasicMultiChannelPerlin.");
        }
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                var noise = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                if (noise != null)
                {
                    noise.m_AmplitudeGain = 0f;
                }
            }
        }
    }
}

