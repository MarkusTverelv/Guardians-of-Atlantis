using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLine : MonoBehaviour
{
    LineRenderer lr;

    public Rigidbody2D[] linePositions;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < linePositions.Length; i++)
        {
            lr.SetPosition(i, linePositions[i].position);
        }
    }
}
