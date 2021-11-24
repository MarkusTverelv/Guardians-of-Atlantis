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
        transform.position = new Vector3(center.transform.position.x, center.transform.position.y, transform.transform.position.z);
        if(miniMap)
        {

        }
    }
}
