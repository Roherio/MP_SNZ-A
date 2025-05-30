using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialCameraPosition;
    private Vector3 initialBackgroundPosition;
    private bool isInitialized = false;

    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f;

    // Distance from camera before activating this background
    public float activationDistance = 30f;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialBackgroundPosition = transform.position;
    }

    void LateUpdate()
    {
        float distanceToCamera = Vector2.Distance(cameraTransform.position, initialBackgroundPosition);

        if (!isInitialized && distanceToCamera < activationDistance)
        {
            // First time the camera gets near — lock in camera reference point
            initialCameraPosition = cameraTransform.position;
            isInitialized = true;
        }

        if (isInitialized)
        {
            Vector3 cameraDelta = cameraTransform.position - initialCameraPosition;

            transform.position = new Vector3(
                initialBackgroundPosition.x + cameraDelta.x * parallaxFactor,
                initialBackgroundPosition.y + cameraDelta.y * parallaxFactor,
                initialBackgroundPosition.z
            );
        }
    }
}