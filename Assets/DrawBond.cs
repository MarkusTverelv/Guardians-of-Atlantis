using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBond : MonoBehaviour
{
    LineRenderer lr;
    public Transform[] linePositions;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < linePositions.Length; i++)
        {
            lr.SetPosition(i, linePositions[i].position);
        }
    }
}
