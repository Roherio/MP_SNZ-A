using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolecable_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("colision");
        if (collision.gameObject.tag == "Player")
        {
            print("colision player");
            //navMenuIG.ints[money] += 100;
            Destroy(gameObject);
        }
    }
}
