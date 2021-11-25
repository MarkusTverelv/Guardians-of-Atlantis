using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject center;
    public bool miniMap;

    private void Start()
    {

        center = GameObject.FindGameObjectWithTag("Center");
    }
    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, center.transform.position);
        Vector2 newPos = Vector2.MoveTowards(transform.position, center.transform.position, distance / 50);
        transform.position = (Vector3)newPos + new Vector3(0, 0, transform.position.z); ;
        
    }
}
