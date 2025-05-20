using UnityEngine;

public class CloudScroller : MonoBehaviour
{
    private float speed = 0.5f;
    public float speedVariation;
    public Transform OriginalPos;
    public Transform FinalPos;

  

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.right * (speed + speedVariation) * Time.deltaTime);

        if (transform.position.x > FinalPos.position.x)
        {
            transform.position = OriginalPos.position;
        }
    }
}