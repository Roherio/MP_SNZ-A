using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class travelCredits : MonoBehaviour
{
    public bool activeCredits = false;
    public GameObject panelCredits;

    public Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (activeCredits)
        {
            translateCredits();
        }
    }
    public void translateCredits()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 50, Space.World);
    }

    public void resetCredits()
    {
        transform.position = startPos;
    }


}
