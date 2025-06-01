using UnityEngine;


//Script que utilitzem per fer que alguns nuvols es moguin sols i tornin a la posició inicial, es com un "fake" parallax
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