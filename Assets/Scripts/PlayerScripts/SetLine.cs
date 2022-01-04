using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLine : MonoBehaviour
{
    public Rigidbody2D[] linePositions;
    private LineRenderer lr;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        offset = new Vector2(0, -0.15f);
    }

    private void Update()
    {
        for (int i = 0; i < linePositions.Length; i++)
            lr.SetPosition(i, linePositions[i].position + offset);
    }
}
