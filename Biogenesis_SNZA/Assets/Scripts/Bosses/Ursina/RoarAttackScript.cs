using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarAttackScript : MonoBehaviour
{
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(20f, 20f, 2f);
        Destroy(gameObject, 1.5f);

        int roarLayer = LayerMask.NameToLayer("RoarAttack");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(roarLayer, enemyLayer, true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime;
    }
}
